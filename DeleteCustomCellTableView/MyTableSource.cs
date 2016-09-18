using System;
using System.Collections.Generic;
using UIKit;

namespace DeleteCustomCellTableView
{
	public class MyTableSource : UITableViewSource
	{
		public delegate void DeleteCellHandler(UITableViewCell cell);
		public event DeleteCellHandler DeleteCell;

		private UITableView thisTable;
		private string CellID = "MyTableCell";
		private List<string> tableItems;

		public MyTableSource ()
		{
			tableItems = new List<string> ();
			for (int i = 0; i < 20; i++) {
				tableItems.Add ("Test Cell " + i.ToString ());
			}
		}

		public void AddNewItem(string itemTitle)
		{
			//Get the insert index
			Foundation.NSIndexPath dIndexPath = Foundation.NSIndexPath.FromItemSection(0,0);
			int insertRowIndex = dIndexPath.Row;
			Console.WriteLine("insertRowIndex = "+insertRowIndex);
			//Add data to source list
			tableItems.Insert (0,itemTitle);
			//Add new row to UI
			thisTable.BeginUpdates();
			thisTable.InsertRows(new Foundation.NSIndexPath[]{dIndexPath},UITableViewRowAnimation.Fade);
			thisTable.EndUpdates();
		}

		public void RemoveAt(int row)
		{
			tableItems.RemoveAt (row);
		}

		#region implement from UITableViewSource

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			thisTable = tableview;
			return tableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			MyTableCell cell = tableView.DequeueReusableCell (CellID) as MyTableCell;
			if (null == cell) {
				//Init custom cell
				cell = new MyTableCell (UITableViewCellStyle.Default, CellID);
				//Bind delete event
				cell.DeleteCell += delegate {
					//Get the correct row
					Foundation.NSIndexPath dIndexPath = tableView.IndexPathForCell(cell);
					int deleteRowIndex = dIndexPath.Row;
					Console.WriteLine("deleteRowIndex = "+deleteRowIndex);
					//Remove data from source list
					RemoveAt(deleteRowIndex);
					//Remove selected row from UI
					tableView.BeginUpdates();
					tableView.DeleteRows(new Foundation.NSIndexPath[]{dIndexPath},UITableViewRowAnimation.Fade);
					tableView.EndUpdates();
				};
			}
			//This code is incorrect
//			cell.DeleteCell += delegate {
//				Console.WriteLine("Delete function began");
//				//Get the correct row
//				Foundation.NSIndexPath dIndexPath = tableView.IndexPathForCell(cell);
//				int deleteRowIndex = dIndexPath.Row;
//				Console.WriteLine("deleteRowIndex = "+deleteRowIndex);
//				//Remove data from source list
//				RemoveAt(deleteRowIndex);
//				//Remove selected row from UI
//				tableView.BeginUpdates();
//				tableView.DeleteRows(new Foundation.NSIndexPath[]{dIndexPath},UITableViewRowAnimation.Fade);
//				tableView.EndUpdates();
//				Console.WriteLine("Delete function ended");
//			};
			cell.Title = tableItems [indexPath.Row];
			return cell;
		}

		#endregion
	}
}

