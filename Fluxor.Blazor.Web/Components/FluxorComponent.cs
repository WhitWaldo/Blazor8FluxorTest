using Fluxor.UnsupportedClasses;
using Microsoft.AspNetCore.Components;
using System;

namespace Fluxor.Blazor.Web.Components
{
	/// <summary>
	/// A component that auto-subscribes to state changes on all <see cref="IStateChangedNotifier"/> properties
	/// and ensures <see cref="ComponentBase.StateHasChanged"/> is called
	/// </summary>
	public abstract class FluxorComponent : ComponentBase, IDisposable
	{
		[Inject]
		private IActionSubscriber ActionSubscriber { get; set; }

		[Inject]
		private IStore Store { get; set; }

		private bool Disposed;
		private IDisposable StateSubscription;
		private readonly ThrottledInvoker StateHasChangedThrottler;

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public FluxorComponent()
		{
			StateHasChangedThrottler = new ThrottledInvoker(() =>
			{
				if (!Disposed)
					InvokeAsync(StateHasChanged);
			});
		}

		/// <summary>
		/// If greater than 0, the feature will not execute state changes
		/// more often than this many times per second. Additional notifications
		/// will be suppressed, and observers will be notified of the latest
		/// state when the time window has elapsed to allow another notification.
		/// </summary>
		protected byte MaximumStateChangedNotificationsPerSecond { get; set; }

		/// <see cref="IActionSubscriber.SubscribeToAction{TAction}(object, Action{TAction})"/>
		public void SubscribeToAction<TAction>(Action<TAction> callback)
		{
			ActionSubscriber.SubscribeToAction<TAction>(this, action =>
			{
				InvokeAsync(() =>
				{
					if (!Disposed)
						callback(action);
					StateHasChanged();
				});
			});
		}

		/// <summary>
		/// Disposes of the component and unsubscribes from any state
		/// </summary>
		public void Dispose()
		{
			if (!Disposed)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
				Disposed = true;
			}
		}

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
			await base.OnParametersSetAsync();

			//Attempt to initialize the store knowing that if it's already been initialized, this won't do anything.
            await Store.InitializeAsync();
        }

        /// <summary>
		/// Subscribes to state properties
		/// </summary>
		protected override void OnInitialized()
		{
			base.OnInitialized();
			StateSubscription = StateSubscriber.Subscribe(this, _ =>
			{
				StateHasChangedThrottler.Invoke(MaximumStateChangedNotificationsPerSecond);
			});
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (StateSubscription is null)
					throw new NullReferenceException(ErrorMessages.ForgottenToCallBaseOnInitialized);

				StateSubscription.Dispose();
				ActionSubscriber?.UnsubscribeFromAllActions(this);
			}
		}
	}
}
