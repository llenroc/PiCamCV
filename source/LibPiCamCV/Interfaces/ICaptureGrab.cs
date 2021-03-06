﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace PiCamCV.Interfaces
{

    public class CaptureProperties
    {
        public int FrameHeight { get;set;}
        public int FrameWidth { get; set; }
        public int Fps { get; set; }
        public bool IsMonochrome { get; set; }

        public override string ToString()
        {
            return string.Format("Width={0}, Height={1}, IsMonochrome={2}, Fps={3}", FrameWidth, FrameHeight, IsMonochrome, Fps);
        }
    }
    public interface ICaptureGrab : ICapture
    {
        event EventHandler ImageGrabbed;

        bool FlipHorizontal { get; set; }
        bool FlipVertical { get; set; }

        void Start();
        void Stop();
        void Pause();
        bool Retrieve(IOutputArray image);

        /// <summary>
        /// Obtain the capture property
        /// </summary>
        /// <param name="index">The index for the property</param>
        /// <returns>The value of the specific property</returns>
        double GetCaptureProperty(CapProp index);

        /// <summary>
        /// Sets the specified property of video capturing
        /// </summary>
        /// <param name="property">Property identifier</param>
        /// <param name="value">Value of the property</param>
        /// <returns>True if success</returns>
        bool SetCaptureProperty(CapProp property, double value);
        
    }

    public static class CaptureGrabExtentions
    {
        /*
         * 
    CV_CAP_PROP_POS_MSEC Current position of the video file in milliseconds.
    CV_CAP_PROP_POS_FRAMES 0-based index of the frame to be decoded/captured next.
    CV_CAP_PROP_POS_AVI_RATIO Relative position of the video file
    CV_CAP_PROP_FRAME_WIDTH Width of the frames in the video stream.
    CV_CAP_PROP_FRAME_HEIGHT Height of the frames in the video stream.
    CV_CAP_PROP_FPS Frame rate.
    CV_CAP_PROP_FOURCC 4-character code of codec.
    CV_CAP_PROP_FRAME_COUNT Number of frames in the video file.
    CV_CAP_PROP_FORMAT Format of the Mat objects returned by retrieve() .
    CV_CAP_PROP_MODE Backend-specific value indicating the current capture mode.
    CV_CAP_PROP_BRIGHTNESS Brightness of the image (only for cameras).
    CV_CAP_PROP_CONTRAST Contrast of the image (only for cameras).
    CV_CAP_PROP_SATURATION Saturation of the image (only for cameras).
    CV_CAP_PROP_HUE Hue of the image (only for cameras).
    CV_CAP_PROP_GAIN Gain of the image (only for cameras).
    CV_CAP_PROP_EXPOSURE Exposure (only for cameras).
    CV_CAP_PROP_CONVERT_RGB Boolean flags indicating whether images should be converted to RGB.
    CV_CAP_PROP_WHITE_BALANCE Currently unsupported
    CV_CAP_PROP_RECTIFICATION Rectification flag for stereo cameras (note: only supported by DC1394 v 2.x backend currently)

         */
        public static CaptureProperties GetCaptureProperties(this ICaptureGrab capture)
        {
            var settings = new CaptureProperties();
            settings.FrameHeight = Convert.ToInt32(capture.GetCaptureProperty(CapProp.FrameHeight));
            settings.FrameWidth = Convert.ToInt32(capture.GetCaptureProperty(CapProp.FrameWidth));
            settings.Fps = Convert.ToInt32(capture.GetCaptureProperty(CapProp.Fps));
            settings.IsMonochrome = Convert.ToBoolean(capture.GetCaptureProperty(CapProp.Monochrome));
            return settings;
        }

        public static void SetCaptureProperties(this ICaptureGrab capture, CaptureProperties properties)
        {
            capture.SetCaptureProperty(CapProp.FrameHeight, properties.FrameHeight);
            capture.SetCaptureProperty(CapProp.FrameWidth, properties.FrameWidth);
        }
    }
}
