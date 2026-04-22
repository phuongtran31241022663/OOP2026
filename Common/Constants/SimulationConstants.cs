using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common.Constants
{
    public static class SimulationConstants
    {
        public const int TickIntervalMs = 2000;      // 2 giây cập nhật tọa độ 1 lần
        public const int InterpolationSteps = 20;    // Chia nhỏ quãng đường để xe chạy mượt
        public const double DefaultSpeedKmh = 40.0;  // Tốc độ xe chạy trung bình trong simulation
        public const double MaxPickupDistanceKm = 5.0; // Khoảng cách tối đa để tìm tài xế
    }
}
