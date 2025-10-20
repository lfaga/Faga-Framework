/***********************************************************************
	Class:				Dictionary
	Author:				Luis Faga
	Created:			23/02/2006
	Description:	
	Inherits:		-
	Version:		1.0		
*************************************************************************/

//Class Dictionary

	// Constructor *********************************
	
		Dictionary = function()
		{
			this.Keys = new ArrayList();
			this.Items = new Object();
		}
	
	
	// Properties **********************************
	
		Dictionary.prototype.Items = null;
		Dictionary.prototype.Keys = null;
	
	
	// Methods *************************************
	
		Dictionary.prototype.Add = function(key, item)
		{
			this.Keys.Add(key);
			this.Items[key] = item;
			return (key);
		}
		
		Dictionary.prototype.Get = function(key)
		{
			return (this.Items[key]);
		}
				
		Dictionary.prototype.Remove = function(key)
		{
			var idx = this.ContainsKey(key);
			
			if (idx != null)
			{
				this.Items[key] = null;
				delete(this.Items[key]);
				this.Keys.Remove(idx);
				this.Keys.Reindex();
			}
		}
		
		Dictionary.prototype.Count = function()
		{
			return (this.Keys.Count());
		}

		Dictionary.prototype.IsEmpty = function()
		{
			return (this.Keys.Count() == 0);
		}

		Dictionary.prototype.Clear = function()
		{
			var c = this.Keys.Length();
			for (var i=0; i<c; i++)
			{
				this.Items[this.Keys.Items[i]] = null;
				delete(this.Items[this.Keys.Items[i]]);
				this.Keys.Remove(i);
			}
		}

		Dictionary.prototype.Contains = function(item)
		{
			for (var id in this)
				if (this.Items[id] == item) return (id);
			return (null);
		}
		
		Dictionary.prototype.ContainsKey = function(key)
		{
			var c = this.Keys.Length();
			for (var i=0; i<c; i++)
				if (this.Keys.Items[i] == key) return (i);
			return (null);
		}


//End Class Control