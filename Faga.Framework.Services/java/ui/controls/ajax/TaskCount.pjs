//Class TaskCount

	// Constructor *********************************
	
	TaskCount = function(id)
	{
		this.setControlHandle(id);
		
		this._serviceURL = this._baseServiceURL + "tareas.asmx/TaskCount";
	}
		
	
	// Inheritance *********************************
	
	TaskCount.prototype = new AJAXControl();


	// Properties **********************************
	
	TaskCount.prototype.taskCount = 0;
	TaskCount.prototype.timerId = null;
	TaskCount.prototype.updateInterval = 10000;	
	
	// Methods *************************************
	
	TaskCount.prototype.updateCount = function()
	{
		var s = "";
		var HideTaskCount = false;

		switch (this.taskCount)
		{
			case 0:
				HideTaskCount = true;
				break;
			case 1:
				s = "1 tarea pendiente.";
				break;
			default:
				s = this.taskCount.toString() + " tareas pendientes.";
				break;
		}
    
		this.controlHandle.innerHTML = "<img src='/images/icons/icon_alert.gif' border=0 align=top>" + s;
		this.controlHandle.style.visibility = (HideTaskCount ? "hidden" : "visible");
		this.controlHandle.style.display = (HideTaskCount ? "none" : "block");
	}
	
	TaskCount.prototype.onLoad = function()
	{
		this.Execute(null, null);
		
		this.timerId = window.setInterval(
				this.getTimerCallback(), this.updateInterval
			);
	}

	TaskCount.prototype.dispose = function()
	{
		window.clearInterval(this.timerId);
	}
	
	TaskCount.prototype.ParseResponse = function(_responseXML)
	{
		try
		{
			if (this.controlHandle != null)
			{
				var objs = _responseXML.getElementsByTagName("int");
				this.taskCount = parseInt(objs[0].text);
				this.updateCount();
			}
		}
		catch (e)
		{
			if (this.controlHandle != null)
				this.controlHandle.innerText = e.message;
		}
	}

	TaskCount.prototype.getTimerCallback = function()
	{
		var parentInstance = this;
		
		return (
			function() {
				parentInstance.Execute(null);
			}
		);
	}	
	
	TaskCount.prototype.toString = function()
	{
		return ("TaskCount object");
	}
	
	
//End Class TaskCount