//Class TabStripButton

	// Constructor *********************************
	
		TabStripButton = function(id)
		{
			this.setControlHandle(id);
			this.TableHandle = document.getElementById(id + "_Body");
			this.ImageHandle = document.getElementById(id + "_ImgDivide");
		}

	
	// Inheritance *********************************
	
		TabStripButton.prototype = new Control();


	// Properties **********************************

		TabStripButton.prototype.Index = 0;
		TabStripButton.prototype.State = 0;
		TabStripButton.prototype.ParentTabStrip = null;
		TabStripButton.prototype.TableHandle = null;
		TabStripButton.prototype.ImageHandle = null;
	
	// Constants ***********************************	

		TabStripButton.prototype.STATE_UNSELECTED = 0;
		TabStripButton.prototype.STATE_SELECTED = 1;
		TabStripButton.prototype.STATE_DISABLED = 2;	
	
	// Methods *************************************
	
		TabStripButton.prototype.onLoad = function()
		{
		}
		
		TabStripButton.prototype.SetState = function(state)
		{
			this.State = state;
	
			if (state == this.STATE_SELECTED)
				this.TableHandle.className = "focused";
			else if (state == this.STATE_UNSELECTED)
				this.TableHandle.className = "unfocused";
			else
				this.TableHandle.className = "disabled";
				
			if (this.Index == 0)
			{
				if (state == this.STATE_SELECTED)
					this.ImageHandle.src = "/images/tabs/l_s.gif";
				else
					this.ImageHandle.src = "/images/tabs/l_u.gif";
			}
			else if (this.Index == this.ParentTabStrip.GetButtonCount() - 1)
			{
				if (this.ParentTabStrip.GetButton(this.Index - 1).State == this.STATE_SELECTED)
					this.ImageHandle.src = "/images/tabs/r_s.gif";
				else
					this.ImageHandle.src = "/images/tabs/r_u.gif";
			}
			else
			{
				if (state == this.STATE_SELECTED)
					this.ImageHandle.src = "/images/tabs/m_u_s.gif";
				else
				{
					if (this.ParentTabStrip.GetButton(this.Index - 1).State == this.STATE_SELECTED)
						this.ImageHandle.src = "/images/tabs/m_s_u.gif";
					else
						this.ImageHandle.src = "/images/tabs/m_u_u.gif";
				}
			}
		}

		TabStripButton.prototype.dispose = function()
		{
			this.TableHandle = null;
			this.ImageHandle = null;
		}

//End Class TabStripButton