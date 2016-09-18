using System;
using UIKit;

namespace DeleteCustomCellTableView
{
	public class AddNewItemViewController : UIViewController
	{
		public delegate void AddItemEventHandler(string content);
		public event AddItemEventHandler AddItemEvent;

		UITextField tfValue = new UITextField();
		UIButton btnSubmit = new UIButton(UIButtonType.System);

		public AddNewItemViewController ()
		{
			this.View.BackgroundColor = UIColor.White;

			tfValue.Frame = new CoreGraphics.CGRect (0, 100, 300, 30);
			tfValue.BackgroundColor = UIColor.Red;
			this.Add (tfValue);

			btnSubmit.Frame = new CoreGraphics.CGRect (0, 150, 300, 50);
			btnSubmit.SetTitle ("Submit", UIControlState.Normal);
			btnSubmit.TouchUpInside += delegate {
				if(null != AddItemEvent){
					AddItemEvent(tfValue.Text);
					this.NavigationController.PopViewController(true);
				}
			};
			this.Add (btnSubmit);
		}
	}
}

