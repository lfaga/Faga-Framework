//Class RequiredCombo

	// Constructor *********************************
	
		RequiredCombo = function(_id, _description)
		{
			this.setControlHandle(_id);
			this.description = _description;	
		}

	
	// Inheritance *********************************
	
		RequiredCombo.prototype = new ValidatedControl();


	// Properties **********************************
	
		RequiredCombo.prototype.val_fail_msg = "Debe seleccionar un valor de la lista.";		
	
	// Events **************************************
	
	// Methods *************************************
	
		RequiredCombo.prototype.onLoad = function()
		{
			this.initValidatedControl(true);
		}

		RequiredCombo.prototype.dispose = function()
		{
			this.controlHandle.onblur = null;
		}
		
		RequiredCombo.prototype.validationFunction = function()
		{
			var val = new String(this.controlHandle.value);
			return (
								(val != "")
						 && (! isNaN(parseInt(val, 10)) )
					);
		}

//End Class RequiredCombo