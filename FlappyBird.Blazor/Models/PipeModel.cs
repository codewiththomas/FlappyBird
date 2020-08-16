using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Blazor.Models
{
    public class PipeModel : INotifyPropertyChanged
    {
        private int _distanceFromLeft = 500;

        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);

        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromLeft)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void Move(int speed)
        {
            DistanceFromLeft -= speed;
        }


        public bool IsCentered()
        {

            //auch wenn der Vogel eigentlich 60px hat, kürze ich das auf 40.
            //schließlich ist er nicht quadratisch. 

            // half of the game width minus the width of the bird
            var centerMin = 500 / 2 - 40;
            // half of the game width plus half the width of the bird
            var centerMax = (500 + 40) / 2;
            return (DistanceFromLeft > centerMin && DistanceFromLeft < centerMax);
        }
    }
}
