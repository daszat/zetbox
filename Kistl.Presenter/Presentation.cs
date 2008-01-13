using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPFPresenter
{
    public class Presentation : INotifyPropertyChanged
    {
        private List<string> slides = new List<string>();
        private int currentIndex = 0;

        public Presentation()
        {
            foreach (Type t in this.GetType().Assembly.GetTypes())
            {
                if (t.Namespace == "WPFPresenter.Slides" && t.IsSubclassOf(typeof(Page)))
                {
                    slides.Add(@"Slides\" + t.Name.Substring(1) + ".xaml");
                }

                slides.Sort();
            }
        }

        public string[] Slides
        {
            get { return this.slides.ToArray(); }
        }

        public string CurrentSlide
        {
            get { return this.slides[this.currentIndex]; }
            set
            {
                CurrentIndex = this.slides.IndexOf(value);
            }
        }

        public int CurrentIndex
        {
            get { return this.currentIndex; }
            set
            {
                if (this.currentIndex != value)
                {
                    this.currentIndex = value;
                    this.OnPropertyChanged("CurrentSlide");
                    this.OnPropertyChanged("CanGoBack");
                    this.OnPropertyChanged("CanGoNext");
                }
            }
        }

        public bool CanGoBack
        {
            get { return this.currentIndex > 0; }
        }

        public bool CanGoNext
        {
            get { return this.currentIndex < this.slides.Count - 1; }
        }

        public void GoBack()
        {
            if (this.CanGoBack)
            {
                this.CurrentIndex--;
            }
        }

        public void GoNext()
        {
            if (this.CanGoNext)
            {
                this.CurrentIndex++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
