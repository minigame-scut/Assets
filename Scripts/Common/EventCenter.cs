using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : MonoBehaviour {

    private static Dictionary<MyEventType, Delegate> m_EventTable = new Dictionary<MyEventType, Delegate>();


    private static void OnAddListener(MyEventType eventType, Delegate callBack) {

        if (!m_EventTable.ContainsKey(eventType))
        {       //没有该类型的数据
            m_EventTable.Add(eventType, null);

        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        { //类型不一致
            throw new Exception(string.Format("尝试为事件{0}添加不一致的委托，当前事件所对应的委托为{1}, 添加的委托事件为{2}", eventType, d.GetType(), callBack.GetType()));

        }
    }


    //无参的监听
    public static void AddListener(MyEventType eventType, CallBack callBack) {

        OnAddListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;

    }
    //一个参数
    public static void AddListener<T>(MyEventType eventType, CallBack<T> callBack)
    {
        OnAddListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;

    }
    //2个参数
    public static void AddListener<T, Y>(MyEventType eventType, CallBack<T,Y> callBack)
    {
        OnAddListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, Y>)m_EventTable[eventType] + callBack;

    }
    public static void AddListener<T, Y ,X>(MyEventType eventType, CallBack<T, Y, X> callBack)
    {
        OnAddListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, Y, X>)m_EventTable[eventType] + callBack;

    }
    public static void AddListener<T, Y, X, Z>(MyEventType eventType, CallBack<T, Y, X, Z> callBack)
    {
        OnAddListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, Y, X, Z>)m_EventTable[eventType] + callBack;

    }


    private static void  OnRemoveListener(MyEventType eventType, Delegate callBack) {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前事件类型为{1}，要移除的为{2}", eventType, d.GetType(), callBack.GetType()));

            }

        }
        else
        {
            throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
        }

    }

    private static void OnRemoveListenered(MyEventType eventType)
    {
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }

    }

    //无参的监听移除
    public static void RemoveListenter(MyEventType eventType, CallBack callBack) {
        OnRemoveListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        OnRemoveListenered(eventType);

    }
    //一个参数的监听移除
    public static void RemoveListenter<T>(MyEventType eventType, CallBack<T> callBack)
    {
        OnRemoveListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        OnRemoveListenered(eventType);

    }
    public static void RemoveListenter<T, X>(MyEventType eventType, CallBack<T, X> callBack)
    {
        OnRemoveListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] - callBack;
        OnRemoveListenered(eventType);

    }
    public static void RemoveListenter<T, X, Y>(MyEventType eventType, CallBack<T, X, Y> callBack)
    {
        OnRemoveListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y>)m_EventTable[eventType] - callBack;
        OnRemoveListenered(eventType);

    }
    public static void RemoveListenter<T, X, Y, Z >(MyEventType eventType, CallBack<T, X, Y, Z> callBack)
    {
        OnRemoveListener(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z>)m_EventTable[eventType] - callBack;
        OnRemoveListenered(eventType);

    }
    //广播
    public static void Broadcast(MyEventType eventType){
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d)) {
            CallBack callBack = d as CallBack; //强制类型转换
            if (callBack != null)
            {
                callBack();
            }
            else {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }

        }
    
    
    }
    //一个参数的广播
    public static void Broadcast<T>(MyEventType eventType, T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>; //强制类型转换
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }

    public static void Broadcast<T, X>(MyEventType eventType, T arg, X arg2)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X> callBack = d as CallBack<T,X>; //强制类型转换
            if (callBack != null)
            {
                callBack(arg, arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }

    public static void Broadcast<T,X, Y>(MyEventType eventType, T arg, X arg2, Y arg3 )
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>; //强制类型转换
            if (callBack != null)
            {
                callBack(arg, arg2, arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }

    public static void Broadcast<T, X, Y, Z>(MyEventType eventType, T arg, X arg2, Y arg3, Z arg4)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>; //强制类型转换
            if (callBack != null)
            {
                callBack(arg, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
            }
        }
    }
}
