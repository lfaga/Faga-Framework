//Class ValidatedControl

	// Constructor *********************************
	
		ValidatedControl = function() {}

	
	// Inheritance *********************************
	
		ValidatedControl.prototype = new Control();


	// Properties **********************************
	
		ValidatedControl.prototype.cssclass = null;
		ValidatedControl.prototype.alerticon = null;
		ValidatedControl.prototype.val_fail_msg = "El valor es incorrecto.";

	// Events **************************************
	
	
	// Methods *************************************
	
		/* MUST IMPLEMENT */
		ValidatedControl.prototype.onLoad = function() {}
				
		/* MUST CALL OnLoad */
		ValidatedControl.prototype.initValidatedControl = function(validateNow)
		{
			this.controlHandle.onblur = this.getThis_OnBlur();
			this.cssclass = this.controlHandle.className;
			this.LoadAlertIcon();
			
			if (validateNow)
				this.validate();
		}

		/* MUST IMPLEMENT */
		ValidatedControl.prototype.dispose = function() {}
		
		ValidatedControl.prototype.validate = function()
		{
			if ( this.validationFunction() )
			{
				this.HideAlertIcon();
				this.controlHandle.className = this.cssclass;
				this.validationOK();
			}
			else
			{
				this.controlHandle.className = 'box_erroneous';
				this.ShowAlertIcon();
				this.validationFailed(this.description + ": " + this.val_fail_msg);
			}
		}
		
		
		ValidatedControl.prototype.getThis_OnBlur = function()
		{
			var parentInstance = this;
			
			return (
				function()
				{
					parentInstance.validate();
				}
			);
		}

		ValidatedControl.prototype.ShowAlertIcon = function()
		{
			this.alerticon.style.visibility = "visible";
			this.alerticon.style.display = "inline";
		}

		ValidatedControl.prototype.HideAlertIcon = function()
		{
			this.alerticon.style.visibility = "hidden";
			this.alerticon.style.display = "none";
		}
		
		ValidatedControl.prototype.LoadAlertIcon = function()
		{
			var pe = this.controlHandle.parentElement;
			var img = document.createElement("IMG");
			img.setAttribute("src", "/images/icons/icon_alert.gif");
			img.setAttribute("alt", this.val_fail_msg);
			img.style.position = "relative";
			img.style.top = "3px";
			pe.appendChild(img);
			this.alerticon = img;
			this.HideAlertIcon();
		}

//End Class ValidatedControl