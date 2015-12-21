using System;

public struct Maybe<T> {
	private readonly bool hasValue;
	public bool HasValue { get { return hasValue; } }

	private readonly T value;
	public T Value {
		get {
			if (!hasValue) {
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
		if (value == null) {
			this.hasValue = false;
			this.value = default(T);
		} else {
			this.hasValue = hasValue;
			this.value = value;
		}
	}

	public delegate void F(T value);

	public void IfPresent(F f) {
		if (hasValue) {
			f(value);
		}
	}

	public static implicit operator Maybe<T>(T value) {
		return new Maybe<T>(true, value);
	}
}