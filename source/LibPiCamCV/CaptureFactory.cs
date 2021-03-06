﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    public enum CaptureDevice
    {
        Unknown = 0,

        /// <summary>
        /// EmguCV. Windows or Pi USB connected camera
        /// </summary>
        Usb =1,

        /// <summary>
        /// Will use Pi Camera module on a Pi. USB on Windows.
        /// </summary>
        Pi = 2
    }

    public static class CaptureFactory
    {
        static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public static ICaptureGrab GetCapture(CaptureDevice device)
        {
            Log.Info(m => m("CV Library={0}", CvInvokeRaspiCamCV.CVLibrary));
            Log.Info(m => m("Getting {0} camera", device));
            if (device == CaptureDevice.Pi)
            {
                return new CapturePi();
            }
            else
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    Log.Warn("You are in Unix but aren't requesting a Pi camera? Whatever you say boss...");
                    if (CvInvokeRaspiCamCV.CVLibrary.Contains("opencv"))
                    {
                        Log.Warn("You are Unix but trying to bind to opencv libraries - not raspicamcv");
                    }
                }
                return new CaptureUsb();
            }
        }
    }
}
