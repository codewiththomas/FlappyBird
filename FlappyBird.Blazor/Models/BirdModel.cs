using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Blazor.Models
{
    /// <summary>
    /// Klasse, welche die Eigenschaften und Methoden des "Bird" Elements
    /// enthält.
    /// </summary>
    public class BirdModel : INotifyPropertyChanged
    {
        public int _distanceFromBottom = 100;

        public event PropertyChangedEventHandler PropertyChanged;

        public int DistanceFromBottom
        {
            get { return _distanceFromBottom; }
            set
            {
                _distanceFromBottom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromBottom)));
            }
        }


        public int JumpStrength { get; set; } = 50;



        public void Fall(int gravity)
        {
            DistanceFromBottom -= gravity;
        }



        public void ResetBird()
        {
            DistanceFromBottom = 100;
        }


        public void Jump()
        {
            if (DistanceFromBottom < 530)
            {
                DistanceFromBottom += JumpStrength;
            }
        }
    }
}
