//Class StatusBar

	// Constructor *********************************
	
	StatusBar = function(_id)
	{
		this.setControlHandle(_id);
		this.messages = new Dictionary();
	}
	
	
	// Inheritance *********************************
	
	StatusBar.prototype = new Control();


	// Properties **********************************
	
	StatusBar.prototype.messages = null;
	StatusBar.prototype.flashcount = 0;
	StatusBar.prototype.flashing = false;
	

	// Events **************************************
	
	//StatusBar.prototype.onEvent = function(source, eventargs) {}
	
	
	// Methods *************************************
	
	StatusBar.prototype.onLoad = function()
	{
	}

	StatusBar.prototype.dispose = function()
	{
		this.messages.Clear();
		this.messages = null;
	}
	
	StatusBar.prototype.Clear = function()
	{
		enu = new Enumerator(this.controlHandle.childNodes);
		for (; ! enu.atEnd(); enu.moveNext())
			this.controlHandle.removeChild(enu.item());
		
		this.controlHandle.innerHTML = "";
	}
	
	StatusBar.prototype.AddError = function(msg)
	{
		var c = document.createElement("SPAN");
		c.appendChild( this.getIcon("no") );
		c.appendChild( this.createText(msg, "error") );
		
		var k = this.messages.Count() + 1;
		var i = this.messages.Add(k.toString(), c);
		
		this.UpdateErrorCount();
		return (i);
	}
	
	StatusBar.prototype.RemoveError = function(errId)
	{
		if (this.messages != null)
		{
			this.messages.Remove(errId);
			this.UpdateErrorCount();
		}
	}
	
	StatusBar.prototype.UpdateErrorCount = function(msg)
	{
		this.Clear();
		var s = "";
		var cnt = this.messages.Count();
		
		if (cnt > 0)
		{
			if (cnt == 1)
				s = "Existe un error.";
			else
				s = "Existen " + (this.messages.Count()).toString() + " errores.";
			
			var a = this.createLink(this.getThis_OnClick(), s, "error");
				
			this.controlHandle.appendChild( this.getIcon("no") );
			this.controlHandle.appendChild(a);

			if (this.flashcount == 0)
				this.flash();
		}
	}
	
	StatusBar.prototype.getThis_OnClick = function()
	{
		var parentInstance = this;
		
		return (
			function()
			{
				var msgs = parentInstance.messages;
				parentInstance.page.ShowAlert(msgs);
			}
		);
	}
	
	StatusBar.prototype.ShowError = function(msg)
	{
		this.Clear();
		this.messages.Clear();
		this.controlHandle.appendChild( this.getIcon("no") );
		this.controlHandle.appendChild( this.createText(msg, "error") );
		if (this.flashcount == 0)
			this.flash();
	}

	StatusBar.prototype.ShowSuccess = function(msg)
	{
		this.Clear();
		this.messages.Clear();
		this.controlHandle.appendChild( this.getIcon("ok") );
		this.controlHandle.appendChild( this.createText(msg, "success") );
	}
	
	StatusBar.prototype.ShowMessage = function(msg)
	{
		this.Clear();
		this.messages.Clear();
		this.controlHandle.appendChild( this.createText(msg, "information") );
	}
	
	StatusBar.prototype.createText = function(msg, cssclass)
	{
		var s = document.createElement("SPAN");
		s.className = cssclass;
		s.innerHTML = msg;
		return (s);
	}
	
	StatusBar.prototype.createLink = function(action, msg, cssclass)
	{
		var s = document.createElement("A");
		s.className = cssclass;
		s.innerHTML = msg;
		s.href = "#";
		s.onclick = action;
		return (s);
	}
	
	StatusBar.prototype.getIcon = function(icon)
	{
		var i = document.createElement("IMG");
		i.src = "/images/icons/icon_alert_" + icon + ".gif";
		i.border = 0;
		i.align = "top";
		i.style.marginRight = "4px";
		return (i);
	}
	
	StatusBar.prototype.flash = function()
	{
		if (! this.flashing)
		{
			this.flashing = true;
			window.setTimeout(this.GetFlashHandle(), 250);
		}
	}
	
	StatusBar.prototype.GetFlashHandle = function()
	{
		var parentInstance = this;
		
		return (
			function()
			{
				if (parentInstance.flashcount++ > 5)
				{
					parentInstance.controlHandle.style.visibility = "visible";
					parentInstance.flashcount = 0;
					parentInstance.flashing = false;
				}
				else
				{
					if (parentInstance.controlHandle.style.visibility == "visible")
						parentInstance.controlHandle.style.visibility = "hidden";
					else
						parentInstance.controlHandle.style.visibility = "visible";
						
					window.setTimeout(parentInstance.GetFlashHandle(), 250);
				}
			}
		);
	}

//End Class StatusBar