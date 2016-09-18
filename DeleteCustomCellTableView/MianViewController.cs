using System;
using UIKit;
using CoreGraphics;

namespace DeleteCustomCellTableView
{
	public class MianViewController : UIViewController
	{
		public MianViewController ()
		{
			this.View.BackgroundColor = UIColor.White;

			//Create a custom table source
			MyTableSource mySource = new MyTableSource ();

			//Create a UITableView
			UITableView tableView = new UITableView ();
			tableView.Frame = this.View.Bounds;
			tableView.Source = mySource;
			tableView.RowHeight = 50;
			this.Add (tableView);

			this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Add",UIBarButtonItemStyle.Done,delegate {
				AddNewItemViewController newItemController = new AddNewItemViewController();
				newItemController.AddItemEvent += (content)=> {
					mySource.AddNewItem(content);
				};
				this.NavigationController.PushViewController(newItemController,true);
			}),true);

			this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem("Refresh",UIBarButtonItemStyle.Done,delegate {
				tableView.ReloadData();
			}),true);
		}
	}
}

