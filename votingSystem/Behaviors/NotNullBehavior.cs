using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace votingSystem.Behaviors
{
    public class NotNullBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry sender)
        {
            base.OnAttachedTo(sender);
            sender.TextChanged += Bindable_textChanged;
        }

        public void Bindable_textChanged(object sender, TextChangedEventArgs args)
        {
            var _sender = sender as Entry;
            _sender.BackgroundColor = string.IsNullOrEmpty(_sender.Text) ? Color.Red : Color.Default;
        }

        protected override void OnDetachingFrom(Entry sender)
        {
            base.OnDetachingFrom(sender);
            sender.TextChanged -= Bindable_textChanged;
        }
    }
}
