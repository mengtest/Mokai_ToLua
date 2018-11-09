/* 
 LuaFramework Code By Jarjin lee
*/

using System;
using System.Collections.Generic;

public class Controller : IController {
    protected IDictionary<string, Type> m_commandMap;
    protected IDictionary<IView, List<string>> m_viewCmdMap;

    protected static volatile IController m_instance;
    protected readonly object m_syncRoot = new object();
    protected static readonly object m_staticSyncRoot = new object();

    protected Controller() {
        InitializeController();
    }

    static Controller() {
    }

    public static IController Instance {
        get {
            if (m_instance == null) {
                //lock 关键字可以用来确保代码块完成运行，而不会被其他线程中断。它可以把一段代码定义为互斥段（critical section）
                //互斥段在一个时刻内只允许一个线程进入执行，而其他线程必须等待。这是通过在代码块运行期间为给定对象获取互斥锁来实现的。
                //在多线程中，每个线程都有自己的资源，但是代码区是共享的，即每个线程都可以执行相同的函数。
                //这可能带来的问题就是几个线程同时执行一个函数，导致数据的混乱，产生不可预料的结果，因此我们必须避免这种情况的发生。
                lock (m_staticSyncRoot) {
                    if (m_instance == null) m_instance = new Controller();
                }
            }
            return m_instance;
        }
    }

    protected virtual void InitializeController() {
        m_commandMap = new Dictionary<string, Type>();
        m_viewCmdMap = new Dictionary<IView, List<string>>();
    }

    /// <summary>
    /// 这个函数不需要区分到低是ViewCommand还是NormalCommand，
    /// 但是同时也就要求了，Command不可以重名，否则优先NormalCommand
    /// 
    /// 这里写的优点多余。我不太喜欢这种直接用字符串索引的，虽然好处是Lua也可以用。
    /// 如果是纯C#，我还是推荐ExecuteCommand<T>() where T ICommand这种形式的写法。
    /// 
    /// 这种写法，只有注册的，才能生效，
    /// 而ExecuteCommand<T>()压根就不需要注册，是非常方便的命令模式。
    /// 
    /// </summary>
    /// <param name="note"></param>
    public virtual void ExecuteCommand(IMessage note) {
        Type commandType = null;
        List<IView> views = null;
        lock (m_syncRoot) {
            if (m_commandMap.ContainsKey(note.Name)) {
                commandType = m_commandMap[note.Name];
            } else {
                views = new List<IView>();
                foreach (var de in m_viewCmdMap) {
                    if (de.Value.Contains(note.Name)) {
                        views.Add(de.Key);
                    }
                }
            }
        }
        if (commandType != null) {  //Controller
            object commandInstance = Activator.CreateInstance(commandType);
            if (commandInstance is ICommand) {
                ((ICommand)commandInstance).Execute(note);
            }
        }
        if (views != null && views.Count > 0) {
            for (int i = 0; i < views.Count; i++) {
                views[i].OnMessage(note);
            }
            views = null;
        }
    }

    public virtual void RegisterCommand(string commandName, Type commandType) {
        lock (m_syncRoot) {
            m_commandMap[commandName] = commandType;
        }
    }

    public virtual void RegisterViewCommand(IView view, string[] commandNames) {
        lock (m_syncRoot) {
            if (m_viewCmdMap.ContainsKey(view)) {
                List<string> list = null;
                if (m_viewCmdMap.TryGetValue(view, out list)) {
                    for (int i = 0; i < commandNames.Length; i++) {
                        if (list.Contains(commandNames[i])) continue;
                        list.Add(commandNames[i]);
                    }
                }
            } else {
                m_viewCmdMap.Add(view, new List<string>(commandNames));
            }
        }
    }

    public virtual bool HasCommand(string commandName) {
        lock (m_syncRoot) {
            return m_commandMap.ContainsKey(commandName);
        }
    }

    public virtual void RemoveCommand(string commandName) {
        lock (m_syncRoot) {
            if (m_commandMap.ContainsKey(commandName)) {
                m_commandMap.Remove(commandName);
            }
        }
    }

    public virtual void RemoveViewCommand(IView view, string[] commandNames) {
        lock (m_syncRoot) {
            if (m_viewCmdMap.ContainsKey(view)) {
                List<string> list = null;
                if (m_viewCmdMap.TryGetValue(view, out list)) {
                    for (int i = 0; i < commandNames.Length; i++) {
                        if (!list.Contains(commandNames[i])) continue;
                        list.Remove(commandNames[i]);
                    }
                }
            }
        }
    }
}

