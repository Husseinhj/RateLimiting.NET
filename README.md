# RateLimiting.NET [![Build Status](https://travis-ci.org/Husseinhj/RateLimiting.NET.svg?branch=master)](https://travis-ci.org/Husseinhj/RateLimiting.NET) ![Nuget version](https://img.shields.io/nuget/v/RateLimiting.svg?style=flat) ![downloads](https://img.shields.io/nuget/dt/RateLimiting.svg?style=flat)

This library help you use `Throttle` and `Debounce` in your *.NET* projects.
For see what is **rate limiting** you can run sample app or [watch online sample](http://demo.nimius.net/debounce_throttle/).

This library is Extension on `object` type which can use for any type.

Read the article about [rate-limiting](https://medium.com/@hussein.juybari/rate-limiting-in-net-6d568acbde08) in medium.

## Nuget
This available on [Nuget Packge Manager](https://www.nuget.org/packages/RateLimiting)
> `PM> Install-Package RateLimiting`

## Debounce method
This method give last object when `interval` **argument** was fire time tick. *For example:* You have 20 item received less than 200 milisecond so `debounceAction` invoked after 20th item received and wait for another item for 200 milisecond.
```csharp
private void OnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
{
	pointerRoutedEventArgs.Pointer.Debounce(interval: 200, debounceAction: delegate(object o)
	{
		//Do any thing here you want in background

		//For use UI code use ---> await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { });
	});
}
```

## Throttle method
This method give last object when `interval` **argument** was fire time tick. *For example:* You have 20 item received and 3 of them become after 200 milisecond, So `throttleAction` invoked when timer fired tick after 200 miliescond and get 3 item and invoked `throttleAction` callback.
```csharp
private void OnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
{
	pointerRoutedEventArgs.Pointer.Throttle(interval: 200, throttleAction: delegate(object o)
	{
	    //Do any thing here you want in background

	    //For use UI code use ---> await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { });
    	});
}
```

## Example screenshot

![Rate Limiting example screenshot](https://raw.githubusercontent.com/Husseinhj/RateLimiting.NET/master/RateLimitingExample/Assets/rate-limiting.PNG)
