//Class Control

	// Constructor *********************************
	
	/* abstract class */
	Control = function() {}
	
	
	// Properties **********************************
	
	Control.prototype.id = null;
	Control.prototype.description = null;
	Control.prototype.page = null;
	Control.prototype.controlHandle = null;
	Control.prototype.sbMessageId = 0;
	Control.prototype.Enabled = true;
	
	
	// Methods *************************************
	
	Control.prototype.setControlHandle = function(id)
	{
		this.id = id;
		var controlHandle = document.getElementById(id);
		controlHandle.controllerObject = this;
		this.controlHandle = controlHandle;
	}
	
	Control.prototype.releaseControlHandle = function()
	{
		if (this.sbMessageId != 0)
			this.page.StatusBar.RemoveError(this.sbMessageId);
		this.page = null;
		this.controlHandle.controllerObject = null;
		this.controlHandle = null;
	}
	
	Control.prototype.validationFailed = function(msg)
	{
		if (this.sbMessageId == 0)
			this.sbMessageId = this.page.StatusBar.AddError(msg);
	}
	
	Control.prototype.validationOK = function()
	{
		if (this.sbMessageId != 0)
		{
			this.page.StatusBar.RemoveError(this.sbMessageId);
			this.sbMessageId = 0;
		}
	}
	
	Control.prototype.isValid = function()
	{
		return (this.sbMessageId == 0);
	}	
	
	Control.prototype.onLoad = function() {}

	Control.prototype.dispose = function() {}
	
	Control.prototype.toString = function()
	{
		return ("Control object");
	}

		
//End Class Control