#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib;
using LuckyTex.Windows;
using LuckyTex.Models;

#endregion

namespace LuckyTex
{
    /// <summary>
    /// LuckyTex Window Extensions Methods
    /// </summary>
    public static class LuckyTexWindowExtensions
    {
        /// <summary>
        /// Show Message Box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isError">Is Error message.</param>
        public static void ShowMessageBox(this string message, bool isError = false)
        {
            MessageWindow window = new MessageWindow();
            window.MessageText = message;
            window.IsError = isError;
            window.ShowDialog();
        }

        public static bool ShowMessageOKCancel(this string message)
        {
            MessageOKCancelWindow window = new MessageOKCancelWindow();
            window.MessageText = message;

            if (window.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShowMessageYesNo(this string message)
        {
            MessageYesNoWindow window = new MessageYesNoWindow();
            window.MessageText = message;

            if (window.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Show Confirm Message Box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isError">Is Error message.</param>
        public static bool ShowConfirmWindow(this string message)
        {
            ConfirmMessageWindow window = new ConfirmMessageWindow();
            window.MessageText = message;
            if (window.ShowDialog() == true)
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Show Log In Box.
        /// </summary>
        /// <param name="value">The object instance can be any object.</param>
        /// <param name="showRemark">Show remark panel.</param>
        /// <returns>Returns LogInInfo if required input is enter.</returns>
        public static LogInInfo ShowLogInBox(this object value, string mcno = "", bool showRemark = false)
        {
            LogInInfo result = null;

            try
            {
                LogInWindow window = new LogInWindow();
                window.Setup(mcno);
                window.ShowRemark = showRemark;
                if (window.ShowDialog() == true)
                {
                    result = new LogInInfo();
                    result.UserName = window.UserName;
                    result.Password = window.Password;
                    result.Remark = window.Remark;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                ex.Message.ToString().ShowMessageBox(true);
            }

            return result;
        }

        public static LogInInfo ShowLogInRemarkBox(this object value)
        {
            LogInInfo result = null;
            LogInWindow window = new LogInWindow();
            window.ShowRemark = true;
            if (window.ShowDialog() == true)
            {
                result = new LogInInfo();
                result.UserName = window.UserName;
                result.Password = window.Password;
                result.Remark = window.Remark;
            }
            return result;
        }

        public static LogInInfo ShowDeleteRemarkBox(this object value)
        {
            LogInInfo result = null;
            DeleteWeavingLotWindow window = new DeleteWeavingLotWindow();
            window.ShowRemark = true;
            if (window.ShowDialog() == true)
            {
                result = new LogInInfo();
                result.UserName = window.UserName;
                result.Password = window.Password;
                result.Remark = window.Remark;
            }
            return result;
        }

        public static ClearCutInfo ShowClearCutBox(this object value, string mcno = "", bool showRemark = true)
        {
            ClearCutInfo result = null;

            try
            {
                ClearCutWindow window = new ClearCutWindow();
                window.Setup(mcno);
                window.ShowRemark = showRemark;
                if (window.ShowDialog() == true)
                {
                    result = new ClearCutInfo();
                    result.UserName = window.UserName;
                    result.Password = window.Password;
                    result.Remark = window.Remark;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                ex.Message.ToString().ShowMessageBox(true);
            }

            return result;
        }

        public static SpecificMCStop ShowSpecificMCStopBox(this object value, string warpheadNo = "", string WarperLot = "", string Operator = "")
        {
            SpecificMCStop result = null;
            SpecificMCStopWindow window = new SpecificMCStopWindow();
            window.Setup(warpheadNo,WarperLot, Operator);

            if (window.ShowDialog() == true)
            {
                result = new SpecificMCStop();
                result.ChkStatus = window.ChkStatus;
                result.Result = window.Result;
            }
            return result;
        }

        public static BeamingSpecificMCStop ShowBeamingSpecificMCStopBox(this object value, string BeamerNo = "", string BeamLot = "", string Operator = "")
        {
            BeamingSpecificMCStop result = null;
            BeamingSpecificMCStopWindow window = new BeamingSpecificMCStopWindow();
            window.Setup(BeamerNo, BeamLot, Operator);

            if (window.ShowDialog() == true)
            {
                result = new BeamingSpecificMCStop();
                result.ChkStatus = window.ChkStatus;
                result.Result = window.Result;
            }
            return result;
        }

        public static ClearPallet ShowClearPalletBox(this object value, string palletNo = "", DateTime? receiveDate = null, string Operator = "")
        {
            ClearPallet result = null;
            ClearPalletWindow window = new ClearPalletWindow();
            window.Setup(palletNo, receiveDate, Operator);

            if (window.ShowDialog() == true)
            {
                result = new ClearPallet();
                result.ChkStatus = window.ChkStatus;
            }
            return result;
        }

        public static bool ShowMCSTOPReasonBox(this object value, string warpheadNo = "", string WarperLot = "")
        {
            bool result = true;
            MCSTOPReasonWindow window = new MCSTOPReasonWindow();
            window.Setup(warpheadNo,WarperLot);

            if (window.ShowDialog() == true)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool ShowBeamingMCSTOPReasonBox(this object value, string P_BEAMERNO = "", string P_BEAMLOT = "")
        {
            bool result = true;
            BeamingMCSTOPReasonWindow window = new BeamingMCSTOPReasonWindow();
            window.Setup(P_BEAMERNO, P_BEAMLOT);

            if (window.ShowDialog() == true)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        // เพิ่มใหม่ใช้กับ finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static LogInInfo ShowLogInSelectPositionBox(this object value,string mcno = "")
        {
            LogInInfo result = null;
            LogInSelectPositionWindow window = new LogInSelectPositionWindow();
            window.Setup(mcno);

            if (window.ShowDialog() == true)
            {
                result = new LogInInfo();
                result.UserName = window.UserName;
                result.Password = window.Password;
                result.Position = window.Position;
            }
            return result;
        }

        public static LogInInfo ShowOldLogInSelectPositionBox(this object value, string mcno = "")
        {
            LogInInfo result = null;
            OldLogInSelectPositionWindow window = new OldLogInSelectPositionWindow();
            window.Setup(mcno);

            if (window.ShowDialog() == true)
            {
                result = new LogInInfo();
                result.UserName = window.UserName;
                result.Password = window.Password;
                result.Position = window.Position;
            }
            return result;
        }


        /// <summary>
        /// Show Defect List Window
        /// </summary>
        /// <param name="value">The object instance can be any object.</param>
        /// <param name="items">The defect list.</param>
        public static void ShowDefectListBox(this object value,
            List<InspectionDefectItem> items, string _inspecionLotNo)
        {
            DefectListWindow window = new DefectListWindow();
            window.Setup(items,_inspecionLotNo);
            if (window.ShowDialog() == true)
            {
            }
        }

        // Old ไม่ได้ใช้งาน
        /// <summary>
        /// Show Test List Window
        /// </summary>
        /// <param name="value">The object instance can be any object.</param>
        /// <param name="items">The test list.</param>
        //public static void ShowInspectionRecordBox(this object value,
        //    List<InspectionTestHistoryItem> items)
        //{
        //    InspectionRecordWindow window = new InspectionRecordWindow();
        //    window.Setup(items);
        //    if (window.ShowDialog() == true)
        //    {
        //    }
        //}

        /// <summary>
        /// New ShowInspectionRecordBox
        /// </summary>
        /// <param name="value"></param>
        /// <param name="items"></param>
        /// <param name="inspecionLotNo"></param>
        /// <param name="testId"></param>
        public static void ShowInspectionRecordBox(this object value,List<InspectionTestHistoryItem> items,
            string inspecionLotNo, string testId)
        {
            InspectionRecordWindow window = new InspectionRecordWindow();
            window.Setup(items,inspecionLotNo, testId);
            if (window.ShowDialog() == true)
            {
            }
        }

        /// <summary>
        /// Show window to input remark.
        /// </summary>
        /// <param name="value">The object instance can be any object.</param>
        /// <param name="value">The exists remark.</param>
        /// <returns>Returns remark info instance.</returns>
        public static RemarkInfo ShowRemarkBox(this object value, string remark = "")
        {
            RemarkInfo result = null;
            RemarkWindow window = new RemarkWindow();
            window.Remark = remark;
            if (window.ShowDialog() == true)
            {
                result = new RemarkInfo();
                result.Remark = window.Remark;
            }
            return result;
        }
        /// <summary>
        /// Show window to confirm grade.
        /// </summary>
        /// <param name="value">The object instance can be any object.</param>
        /// <param name="grade">The grade string.</param>
        /// <param name="session">The current session.</param>
        /// <returns>Returns Confirm Grade info instance.</returns>
        public static ConfirmGradeInfo ShowConfirmGradeBox(this object value, 
            string grade, InspectionSession session)
        {
            ConfirmGradeInfo result = null;

            ConfirmGradeWindow window = new ConfirmGradeWindow();
            window.Setup(grade, session);
            window.ShowDialog();
            result = window.ConfirmResult;

            return result;
        }

        public static ReceiveDetailInfo ShowReceiveDetailBox(this object value)
        {
            ReceiveDetailInfo result = null;
            ReceiveDetailWindow window = new ReceiveDetailWindow();
            if (window.ShowDialog() == true)
            {
                result = new ReceiveDetailInfo();

            }
            return result;
        }

        public static CauseOfWarperStopInfo ShowCauseOfWarperStopBox(this object value)
        {
            CauseOfWarperStopInfo result = null;
            CauseOfWarperStopWindow window = new CauseOfWarperStopWindow();
            if (window.ShowDialog() == true)
            {
                result = new CauseOfWarperStopInfo();

            }
            return result;
        }

        public static CauseOfBeamerStopInfo ShowCauseOfBeamerStopBox(this object value)
        {
            CauseOfBeamerStopInfo result = null;
            CauseOfBeamerStopWindow window = new CauseOfBeamerStopWindow();
            if (window.ShowDialog() == true)
            {
                result = new CauseOfBeamerStopInfo();

            }
            return result;
        }

        public static StartNewInsLotInfo ShowStartNewInsLotBox(this object value)
        {
            StartNewInsLotInfo result = null;
            StartNewInsLotWindow window = new StartNewInsLotWindow();
            if (window.ShowDialog() == true)
            {
                result = new StartNewInsLotInfo();

            }
            return result;
        }

        /// เพิ่มหน้าจอ Load Image Defect
        /// <summary>
        /// ShowViewDefectBox
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pathDefectFile"></param>
        public static void ShowViewDefectBox(this object value, string pathDefectFile)
        {
            ViewDefectWindow window = new ViewDefectWindow();
            window.DefectFile = pathDefectFile;

            if (window.ShowDialog() == true)
            {
            }
        }
    }
}
