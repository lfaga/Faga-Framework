//Class TabStrip

	// Constructor *********************************
	
		TabStrip = function(id, selectedIndex)
		{
			this.setControlHandle(id);
			this.SelectedIndex = selectedIndex;
			this.Buttons = new Dictionary();
			this.ActionsOn = new Dictionary();
			this.ActionsOff = new Dictionary();
		}

	
	// Inheritance *********************************
	
		TabStrip.prototype = new Control();


	// Properties **********************************
	
		TabStrip.prototype.SelectedIndex = 0;
		TabStrip.prototype.Buttons = null;
		TabStrip.prototype.ActionsOn = null;
		TabStrip.prototype.ActionsOff = null;

	// Events **************************************
	
		//TabStrip.prototype.onClick = function(source, eventargs) {}
	
	
	// Methods *************************************
	
		TabStrip.prototype.onLoad = function()
		{
			//this.controlHandle.onclick = this.getThis_OnClick();
		}
		
		TabStrip.prototype.AddButton = function(btn, actionOn, actionOff)
		{
			if ( (btn != undefined) && (btn != null) )
			{
				this.Buttons.Add(btn.id, btn);
				this.ActionsOn.Add(btn.id, actionOn);
				this.ActionsOff.Add(btn.id, actionOff);
				btn.Index = this.GetButtonCount() - 1;
				btn.ParentTabStrip = this;
			}
			return (btn);
		}
		
		TabStrip.prototype.GetButton = function(index)
		{
			for (var id in this.Buttons.Items)
			{
				var btn = this.Buttons.Get(id);
				
				if (btn.Index == index)
					return (btn);
			}
			return (null);
		}
		

		TabStrip.prototype.GetButtonCount = function()
		{
			return (this.Buttons.Count());
		}
		
		
		TabStrip.prototype.SelectTab = function(index)
		{
			this.SelectedIndex = index;

			for (var id in this.Buttons.Items)
			{
				var btn = this.Buttons.Get(id);
				var func;
				
				if (btn.Index == this.SelectedIndex)
				{
					btn.SetState(btn.STATE_SELECTED);
					func = this.ActionsOn.Get(id);
				}
				else
				{
					btn.SetState(btn.STATE_UNSELECTED);
					func = this.ActionsOff.Get(id);
				}
				
				if (func != null)
					func();
			}

			return (false);
		}
		

		TabStrip.prototype.dispose = function()
		{
			this.Buttons.Clear();
			this.Buttons = null;
			this.ActionsOn.Clear();
			this.ActionsOn = null;
			this.ActionsOff.Clear();
			this.ActionsOff = null;
		}

//End Class TabStrip