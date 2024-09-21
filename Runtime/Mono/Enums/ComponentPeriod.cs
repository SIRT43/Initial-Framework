using System;

namespace InitialFramework
{
    /// <summary>  
    /// ��ʾ�����������ڷ�������Щ��������Ϸ����������������������ִֻ��һ�εġ�  
    /// </summary>  
    public enum SinglePeriod : byte
    {
        /// <summary>  
        /// ��ʾ���������ڻ��Ѻ��������õķ�����  
        /// </summary>  
        Awake,
        /// <summary>  
        /// ��ʾ�ڽű�ʵ��������ʱ�Զ����õĳ�ʼ��������  
        /// </summary>  
        Start,
    }

    /// <summary>  
    /// ʹ�� Flags ���Ա�ʾ����ͨ��λ������϶�� <see cref="SinglePeriod"/> ö��ֵ��  
    /// </summary>  
    [Flags]
    public enum SinglePeriodFlags : byte
    {
        /// <summary>  
        /// ��ʾ���������ڻ��Ѻ��������õķ�����  
        /// </summary>  
        Awake = 1,
        /// <summary>  
        /// ��ʾ�ڽű�ʵ��������ʱ�Զ����õĳ�ʼ��������  
        /// </summary>  
        Start = 2,
    }

    /// <summary>  
    /// ��ʾ����Ϸѭ���п����ظ����õ��������ڷ�����  
    /// </summary>  
    public enum RepeatedlyPeriod : byte
    {
        /// <summary>  
        /// ÿ֡����һ�Σ����ڸ�����Ϸ״̬��  
        /// </summary>  
        Update,
        /// <summary>  
        /// �Թ̶���ʱ�������ã�ͨ������������㡣  
        /// </summary>  
        FixedUpdate,
        /// <summary>  
        /// ������ Update ��������֮����ã�����������Ϸ״̬���¡�  
        /// </summary>  
        LateUpdate,
    }

    /// <summary>  
    /// ʹ�� Flags ���Ա�ʾ����ͨ��λ������϶�� <see cref="RepeatedlyPeriod"/> ö��ֵ��  
    /// </summary>  
    [Flags]
    public enum RepeatedlyPeriodFlags : byte
    {
        /// <summary>  
        /// ÿ֡����һ�Σ����ڸ�����Ϸ״̬��  
        /// </summary>  
        Update = 1,
        /// <summary>  
        /// �Թ̶���ʱ�������ã�ͨ������������㡣  
        /// </summary>  
        FixedUpdate = 2,
        /// <summary>  
        /// ������ Update ��������֮����ã�����������Ϸ״̬���¡�  
        /// </summary>  
        LateUpdate = 4,
    }

    /// <summary>  
    /// ���п��ܵ��������ڷ������������ε��ú��ظ����õķ�����  
    /// </summary>  
    public enum Period : byte
    {
        /// <summary>  
        /// ���������ڻ��Ѻ��������õķ�����  
        /// </summary>  
        Awake,
        /// <summary>  
        /// �ڽű�ʵ��������ʱ�Զ����õĳ�ʼ��������  
        /// </summary>  
        Start,
        /// <summary>  
        /// ÿ֡����һ�Σ����ڸ�����Ϸ״̬��  
        /// </summary>  
        Update,
        /// <summary>  
        /// �Թ̶���ʱ�������ã�ͨ������������㡣  
        /// </summary>  
        FixedUpdate,
        /// <summary>  
        /// ������ Update ��������֮����ã�����������Ϸ״̬���¡�  
        /// </summary>  
        LateUpdate
    }

    /// <summary>  
    /// ʹ�� Flags ���Ա�ʾ����ͨ��λ������϶�� <see cref="Period"/> ö��ֵ��  
    /// </summary>  
    [Flags]
    public enum PeriodFlags : byte
    {
        /// <summary>  
        /// ���������ڻ��Ѻ��������õķ�����  
        /// </summary>  
        Awake = 1,
        /// <summary>  
        /// �ڽű�ʵ��������ʱ�Զ����õĳ�ʼ��������  
        /// </summary>  
        Start = 2,
        /// <summary>  
        /// ÿ֡����һ�Σ����ڸ�����Ϸ״̬��  
        /// </summary>  
        Update = 4,
        /// <summary>  
        /// �Թ̶���ʱ�������ã�ͨ������������㡣  
        /// </summary>  
        FixedUpdate = 8,
        /// <summary>  
        /// ������ Update ��������֮����ã�����������Ϸ״̬���¡�  
        /// </summary>  
        LateUpdate = 16
    }
}