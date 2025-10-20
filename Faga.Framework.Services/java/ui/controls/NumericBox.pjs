//Class NumericBox

	// Constructor *********************************
	
		NumericBox = function(_id, _description, _isfloat, _precision)
		{
			this.setControlHandle(_id);
			this.description = _description;			
			this.isfloat = _isfloat;
			this.precision = _precision;
		}

	
	// Inheritance *********************************
	
		NumericBox.prototype = new ValidatedControl();


	// Properties **********************************
	
		NumericBox.prototype.isfloat = true;
		NumericBox.prototype.precision = 2;
	

	// Events **************************************
	
	// Methods *************************************
	
		NumericBox.prototype.onLoad = function()
		{
			this.initValidatedControl(true);
		}

		NumericBox.prototype.dispose = function()
		{
			this.controlHandle.onblur = null;
		}
		
		RequiredBox.prototype.validationFunction = function()
		{
			var val = new String(this.controlHandle.value);
			if (val != "")
			{
				var f = parseFloat(val.replace(",", "."));
				if (! isNaN(f))
				{
					var s = "";
					
					if (! this.isfloat)
						s = f.toFixed(0);
					else
						s = f.toFixed(this.precision);
					
					this.controlHandle.value = s;
					return (true);
				}
				else
					return (false);
			}
			return (true);
		}
		

//End Class NumericBox