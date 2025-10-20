//Class RequiredBox

	// Constructor *********************************
	
		RequiredBox = function(_id, _description)
		{
			this.setControlHandle(_id);
			this.description = _description;	
		}

	
	// Inheritance *********************************
	
		RequiredBox.prototype = new ValidatedControl();


	// Properties **********************************
	
		RequiredBox.prototype.val_fail_msg = "Debe completar este campo.";		
	
	// Events **************************************
	
	// Methods *************************************
	
		RequiredBox.prototype.onLoad = function()
		{
			this.initValidatedControl(true);
		}

		RequiredBox.prototype.dispose = function()
		{
			this.controlHandle.onblur = null;
		}
		
		RequiredBox.prototype.validationFunction = function()
		{
			var val = new String(this.controlHandle.value);
			return (val != "");
		}

//End Class RequiredBox