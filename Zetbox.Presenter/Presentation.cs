// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections;
using System.Resources;
using System.Reflection;

namespace WPFPresenter
{
    public class Presentation : INotifyPropertyChanged
    {
        public const string SlidesFile = @"Slides\Slides.txt";
        private List<string> slides = new List<string>();
        private int currentIndex = 0;

        public Presentation()
        {
            if (System.IO.File.Exists(SlidesFile))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(SlidesFile))
                {
                    while (!sr.EndOfStream)
                        slides.Add(sr.ReadLine());
                }
            }

            List<string> slidesInAssembly = new List<string>();

            Assembly asm = Assembly.GetExecutingAssembly();
            using (ResourceReader reader = new ResourceReader(asm.GetManifestResourceStream(asm.GetName().Name + ".g.resources"))) 
            { 
                foreach (DictionaryEntry entry in reader) 
                {
                    if (entry.Key is string)
                    {
                        string s = ((string)entry.Key);
                        if (s.StartsWith("slides/") && s.EndsWith(".baml"))
                        {
                            slidesInAssembly.Add(s.Replace(".baml", ".xaml"));
                        }
                    }
                } 
            }

            /*foreach (Type t in this.GetType().Assembly.GetTypes())
            {
                if (t.Namespace == "WPFPresenter.Slides" && t.IsSubclassOf(typeof(Page)))
                {
                    string s = @"Slides\" + t.Name.Substring(1) + ".xaml";
                    slidesInAssembly.Add(s);
                }
            }*/

            slidesInAssembly.Sort();

            slidesInAssembly.ForEach(s => { if (!slides.Contains(s)) slides.Add(s); });

            foreach (string s in slides.ToArray())
            {
                if (!slidesInAssembly.Contains(s)) slides.Remove(s);
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(SlidesFile))
            {
                slides.ForEach(f => sw.WriteLine(f));
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
