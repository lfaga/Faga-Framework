//Class DateSelector

	// Constructor *********************************
	
	DateSelector = function(id, allowBlank)
	{
		this.setControlHandle(id);
		this.AllowBlank = allowBlank;
	}
	
	
	// Inheritance *********************************
	
	DateSelector.prototype = new Control();


	// Properties **********************************

	DateSelector.prototype.AllowBlank = false;
	DateSelector.prototype.date = null;
	DateSelector.prototype.txtDate = null;
	DateSelector.prototype.lnkBlank = null;
	
	
	// Events **************************************
	
	DateSelector.prototype.onDateChanged = function(source, eventargs) {}
	
	
	// Methods *************************************
	
	DateSelector.prototype.onLoad = function()
	{
		this.txtDate = document.getElementById(this.id + "_txtDate");
		this.lnkBlank = document.getElementById(this.id + "_lnkBlank");
		
		this.txtDate.onchange = this.gettxtDate_OnChange();	
		
		if (this.lnkBlank != null)
			this.lnkBlank.onclick = this.getlnkBlank_OnClick();
		
		if (this.txtDate.value != "")
		{
			this.parseControlDate();
			this.updateDisplay();
		}
		else
		{
			if (this.AllowBlank)
			{
				this.date = null;
				this.txtDate.value = "";
			}
			else
			{
				this.date = new Date();
				this.updateDisplay();
			}
		}
	}

	DateSelector.prototype.dispose = function()
	{
		this.txtDate = null;
		this.lnkBlank = null;
	}
	
	DateSelector.prototype.toString = function()
	{
		return ("DateSelector object");
	}
	
	DateSelector.prototype.updateDisplay = function()
	{
		var s = "";
		
		if (this.date != null)
		{
			var d = this.date.getDate();
			var m = this.date.getMonth() + 1;
			var y = this.date.getFullYear();
			
			s = d.IntegerDigits(2) + "/"
				+ m.IntegerDigits(2)  + "/"
				+ y.IntegerDigits(4);
		}

		this.txtDate.value = s;
	}
	
	DateSelector.prototype.gettxtDate_OnChange = function()
	{
		var parentInstance = this;
		
		return (
			function()
			{
				parentInstance.parseControlDate();
			}
		);
	}


	DateSelector.prototype.getlnkBlank_OnClick = function()
	{
		var parentInstance = this;
		
		return (
			function()
			{
				parentInstance.date = null;
				parentInstance.txtDate.value = "";
				
				//Raise Event
				parentInstance.onDateChanged(parentInstance, parentInstance.date);
		
				return (false);
			}
		);
	}	
	
	
	DateSelector.prototype.parseControlDate = function()
	{
		if (this.txtDate.value != "")
		{
			var d = this.txtDate.value;
			var dia = parseInt(d.substring(0,2), 10);
			var mes = parseInt(d.substring(3,5), 10) - 1;
			var anio = parseInt(d.substring(6,10), 10);
			d = new Date(anio, mes, dia);
			this.date = d;
		}
		else
			this.date = null;
			
		//Raise Event
		this.onDateChanged(this, this.date);
	}
	
	
//End Class DateSelector