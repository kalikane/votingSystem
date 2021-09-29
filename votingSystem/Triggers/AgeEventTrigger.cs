using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace votingSystem.Triggers
{
    public class AgeEventTrigger : TriggerAction<Entry>
    {

        protected override void Invoke(Entry sender)
        {
            var entry = sender as Entry;
            var flag = int.TryParse(entry.Text, out int age);
            entry.BackgroundColor = ((!flag) || (age < 18 || age > 68)) ? Color.Red : Color.Default;
        }
    }
}
