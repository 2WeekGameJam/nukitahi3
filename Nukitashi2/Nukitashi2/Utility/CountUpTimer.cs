using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Nukitashi2.Utility
{
    class CountUpTimer:Timer
    {
        public CountUpTimer() : base()
        {
            Initialize();
        }

        public CountUpTimer(float second) : base(second)
        {
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = 0.0f;
        }

        public override bool IsTime()
        {
            return currentTime >= limitTime;
        }

        public override void Update(GameTime gameTime)
        {
            currentTime = Math.Min(currentTime + 1, limitTime);
        }
        public override float Rate()
        {
            return currentTime / limitTime;
        }
    }
}
