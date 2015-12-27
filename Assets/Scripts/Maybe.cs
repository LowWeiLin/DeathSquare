using System;

public struct Maybe<T> {
	
	private readonly bool _hasValue;
	public bool HasValue { get { return _hasValue && value != null && !value.Equals(null); } }

	private readonly T value;
	public T Value {
		get {
			if (!_hasValue) {
				throw new InvalidOperationException();
			}
			return value;
		}
	}
		
	public static Maybe<T> Empty {
		get {
			return new Maybe<T>(false, default(T));
		}
	}

	public static Maybe<T> Of(T value) {
		return new Maybe<T>(true, value);
	}
		
	private Maybe(bool hasValue, T value) {
		if (value == null || value.Equals(null)) {
			this._hasValue = false;
			this.value = default(T);
		} else {
			this._hasValue = hasValue;
			this.value = value;
		}
	}

	public void IfPresent(Action<T> f) {
		if (HasValue) {
			f(value);
		}
	}

	public T OrElse(T alt) {
		if (HasValue) {
			return value;
		} else {
			return alt;
		}
	}

	public Maybe<U> Map<U>(Func<T, U> f) {
		if (HasValue) {
			return f(value);
		} else {
			return new Maybe<U>(false, default(U));
		}
	}

	public Maybe<U> FlatMap<U>(Func<T, Maybe<U>> f) {
		if (HasValue) {
			return f(value);
		} else {
			return new Maybe<U>(false, default(U));
		}
	}

	public static implicit operator Maybe<T>(T value) {
		return new Maybe<T>(true, value);
	}
}