using FlappyBird.Blazor.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Blazor.Models
{
    /// <summary>
    /// Klasse, welche für den jeweiligen GameState zuständig ist.
    /// </summary>
    public class GameManager : INotifyPropertyChanged
    {


        public GameState GameState { get; private set; } = GameState.TitleScreen;

        public BirdModel Bird { get; private set; }

        public ObservableCollection<PipeModel> Pipes { get; private set; }


        //Pixel pro Tick, die das Bird-Objekt nach unten fällt
        private readonly int _gravity = 2;

        //Pixel pro Tick, die sich die Pipes nach links bewegen (simuliert die Geschwindigkeit von Bird)
        private readonly int _speed = 2;


        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Konstruktor
        /// </summary>
        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new ObservableCollection<PipeModel>();
            Pipes.CollectionChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipes)));
        }



        private async void MainLoop()
        {
            while(GameState == GameState.Running)
            {
                Bird.Fall(_gravity);
                foreach (var pipe in Pipes)
                {
                    pipe.Move(_speed);
                }
                
                if (Bird.DistanceFromBottom <= 0)
                {
                    GameOver();
                }

                if (!Pipes.Any() || Pipes.Last().DistanceFromLeft < 250)
                {
                    GeneratePipe();
                }

                if (Pipes.First().DistanceFromLeft < -60)
                {
                    Pipes.Remove(Pipes.First());
                }

                var centeredPipe = GetCenteredPipe();
                if (centeredPipe != null)
                {
                    // pipe height - ground height + pipe distance from bottom
                    var min = 300 - 150 + centeredPipe.DistanceFromBottom;
                    // pipe gap - ground height + pipe distance from bottom - height of bird
                    var max = 430 - 150 + centeredPipe.DistanceFromBottom - 45;
                    if (Bird.DistanceFromBottom < min || Bird.DistanceFromBottom > max)
                    { 
                        GameOver();
                    }
                }

                await Task.Delay(20); // 20ms warten, bis Bird wieder fällt
            }

            if (GameState == GameState.GameOver)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));
            }
        }



        public void StartNewGame()
        {
            Bird.ResetBird();
            Pipes.Clear();

            GameState = GameState.Running;
            MainLoop();
        }


        public void GameOver()
        {
            GameState = GameState.GameOver;
        }


        private void GeneratePipe()
        {
            Pipes.Add(new PipeModel());
        }

        private PipeModel GetCenteredPipe()
        {
            return Pipes.FirstOrDefault(p => p.IsCentered());
        }
    }
}
