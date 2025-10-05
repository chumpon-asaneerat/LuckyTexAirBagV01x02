#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

using NLib;

using LuckyTex.Pages;

#endregion

namespace LuckyTex.Services
{
    #region StatusMessage EventHandler and EventArgs

    /// <summary>
    /// Status Message Event Args
    /// </summary>
    public class StatusMessageEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StatusMessageEventArgs()
            : base()
        {
            this.Message = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the status messsage.
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
    /// <summary>
    /// Status Message Event Handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The EventArgs.</param>
    public delegate void StatusMessageEventHandler(object sender, StatusMessageEventArgs e);

    #endregion

    #region Page Manager

    /// <summary>
    /// Application GUI (Page) Manager class.
    /// </summary>
    public class PageManager
    {
        #region Singelton Access

        private static PageManager _instance = null;
        /// <summary>
        /// Singelton access instance.
        /// </summary>
        public static PageManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(PageManager))
                    {
                        _instance = new PageManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Varaibles

        private Stack<ContentControl> _currents = new Stack<ContentControl>();

        private DateTime _lastStatusUpdate = DateTime.Now;

        private DispatcherTimer _timer = null;
        private int _timerInterval = 1; // timer in second.

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private PageManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PageManager()
        {
            try
            {
                if (null != _currents)
                    _currents.Clear();
                _currents = null;
            }
            catch { }
            finally
            {
                Shutdown();
            }
        }

        #endregion

        #region Public Methods

        #region Start/Shutdown

        /// <summary>
        /// Start service.
        /// </summary>
        public void Start()
        {
            InitTimer();
        }
        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            ReleaseTimer();
            Close();
        }

        #endregion

        #region Z-Order managements

        /// <summary>
        /// Step back one sub screen.
        /// </summary>
        public void Back()
        {
            if (null != _currents && _currents.Count > 0)
            {
                ContentControl last = _currents.Pop();
                if (null != last)
                {
                    Console.Write("pup last object out");
                }
                // Raise event
                ContentChanged.Call(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Step back one sub screen.
        /// </summary>
        public void Back2()
        {
            if (null != _currents && _currents.Count > 0)
            {
                if (_currents.Count > 2)
                {
                    _currents.Pop();
                    ContentControl last = _currents.Pop();
                    if (null != last)
                    {
                        Console.Write("pup last object out");
                    }
                }
                // Raise event
                ContentChanged.Call(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Step back one sub screen.
        /// </summary>
        public void Back3()
        {
            if (null != _currents && _currents.Count > 0)
            {
                if (_currents.Count > 3)
                {
                    _currents.Pop();
                    _currents.Pop();
                    ContentControl last = _currents.Pop();
                    if (null != last)
                    {
                        Console.Write("pup last object out");
                    }
                }
                // Raise event
                ContentChanged.Call(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Close all sub screens.
        /// </summary>
        public void Close()
        {
            try
            {
                if (_currents != null)
                {
                    lock (this)
                    {
                        _currents.Clear();
                    }

                    if (_currents.Count == 0)
                    {
                        // Raise event
                        ContentChanged.Call(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion

        #region Status Messages

        /// <summary>
        /// Update Status Message
        /// </summary>
        /// <param name="format">The status message that support format</param>
        /// <param name="args">The arguments for format.</param>
        public void UpdateStatusMessage(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            // set last status update time.
            _lastStatusUpdate = DateTime.Now;

            if (null != StatusUpdated)
            {
                // Raise event.
                StatusUpdated.Call(this, new StatusMessageEventArgs() { Message = msg });
            }
        }

        #endregion

        #region Timer methids

        private void InitTimer()
        {
            if (null != _timer)
                return;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(_timerInterval);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        private void ReleaseTimer()
        {
            if (null != _timer)
            {
                _timer.Tick -= new EventHandler(_timer_Tick);
                _timer.Stop();
            }
            _timer = null;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (null != OnTick)
            {
                OnTick.Call(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region Public Properties

        #region For Page Z-Order managements

        /// <summary>
        /// Gets or sets current content control that need to display on main window's
        /// content area.
        /// </summary>
        public ContentControl Current
        {
            get
            {
                if (null != _currents && _currents.Count > 0)
                {
                    lock (this)
                    {
                        return _currents.Peek();
                    }
                }
                else return null;
            }
            set
            {
                if (null == value)
                    return;

                ContentControl last = null;
                if (null != _currents && _currents.Count > 0)
                {
                    lock (this)
                    {
                        last = _currents.Peek();
                    }
                }

                Type lastPageType = (null != last) ? last.GetType() : null;

                Type newPageType = value.GetType();

                if (lastPageType != newPageType)
                {
                    lock (this)
                    {
                        if (null == _currents)
                        {
                            _currents = new Stack<ContentControl>();
                        }
                        // keep to stack
                        _currents.Push(value);
                    }
                    // Raise event
                    ContentChanged.Call(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets number of content page is on Z order stack.
        /// </summary>
        public int Count
        {
            get
            {
                if (null == _currents)
                    return 0;
                else return _currents.Count;
            }
        }

        #endregion

        #region Status Message

        /// <summary>
        /// Gets is last Status message date and time.
        /// </summary>
        public DateTime LastStatusUpdate { get { return _lastStatusUpdate; } }

        #endregion

        #endregion

        #region Public Events

        /// <summary>
        /// ContentChanged event.
        /// </summary>
        public event System.EventHandler ContentChanged;
        /// <summary>
        /// StatusUpdated event.
        /// </summary>
        public event StatusMessageEventHandler StatusUpdated;
        /// <summary>
        /// OnTick Event.
        /// </summary>
        public event System.EventHandler OnTick;

        #endregion
    }

    #endregion
}