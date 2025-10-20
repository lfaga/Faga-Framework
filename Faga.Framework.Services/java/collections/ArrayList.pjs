/***********************************************************************
	Class:				ArrayList
	Author:				Luis Faga
	Created:			23/02/2006
	Description:	
	Inherits:		-
	Version:		1.0		
*************************************************************************/

//Class ArrayList

	// Constructor *********************************
	
		ArrayList = function()
		{
			this.Items = new Array();
		}	
	
	// Properties **********************************
	
		ArrayList.prototype.Items = null;
	
	
	// Methods *************************************
	
		ArrayList.prototype.Add = function(item)
		{
			var i = this.Count();
			this.Items[i] = item;
			return (i);
		}
		
		ArrayList.prototype.Get = function(index)
		{
			return (this.Items[index]);
		}
		
		ArrayList.prototype.Remove = function(index)
		{
			this.Items[index] = null;
			delete(this.Items[index]);
		}

		ArrayList.prototype.Count = function()
		{
			var cnt = 0;
			for (var i in this.Items) cnt++;
			return (cnt);
		}

		ArrayList.prototype.Length = function()
		{
			return (this.Items.length);
		}

		ArrayList.prototype.IsEmpty = function()
		{
			return (this.Count() == 0);
		}

		ArrayList.prototype.Clear = function()
		{
			for (i=0; i<this.Length(); i++)
			{
				this.Items[i] = null;
				delete(this.Items[i]);
			}
		}

		ArrayList.prototype.Contains = function(item)
		{
			for (i=0; i<this.Length(); i++)
				if (this.Items[i] == item) return (true);
			return (false);
		}
		
		ArrayList.prototype.Reindex = function()
		{
			if (this.Length() > 0)
			{
				var na = new Array();
				
				for (i=0; i<this.Length(); i++)
				{
					if (this.Items[i] != null)
						na.push( this.Items[i] );
				}
				
				this.Items = na;
			}
		}
		
		
//End Class Control