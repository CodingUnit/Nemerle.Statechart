using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Subjects;

namespace CommonLib.Macros
{
  public class ChangeObservable<T> : IObservable<T>
  {
    T _elem;
    bool init;
    IObservable<T> _source;
    Func<T, T, bool> _changed;
    Func<T, bool> _old_set;

    public ChangeObservable(IObservable<T> src, Func<T, bool> old_set, Func<T, T, bool> changed)
    {
      _source = src;
      _old_set = old_set;
      _changed = changed;
    }

    bool changed(T old, T nelem)
    {
      if (_changed != null)
      {
        return _changed(old, nelem);
      }
      else
      {
         return !old.Equals(nelem);
      }
    }

    void Check(T item, IObserver<T> obs)
    {
      if (_old_set == null || !init || _old_set(_elem))
        if (!init || changed(_elem, item))
        {
          _elem = item;
          init = true;
          obs.OnNext(item);
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
      return _source.Subscribe(x => Check(x, observer));
    }
  }

}
