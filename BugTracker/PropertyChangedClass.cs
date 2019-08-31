using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BugTracker
{
    public class PropertyChangedClass : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var property in propertyNames)
                OnPropertyChanged(property);
        }
    }
}