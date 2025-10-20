//Class LinkButton

	// Constructor *********************************
	
		LinkButton = function(_id)
		{
			this.setControlHandle(_id);
		}

	
	// Inheritance *********************************
	
		LinkButton.prototype = new Control();


	// Properties **********************************
	
		//LinkButton.prototype.isfloat = true;
	

	// Events **************************************
	
		LinkButton.prototype.onClick = function(source, eventargs) {}
	
	
	// Methods *************************************
	
		LinkButton.prototype.onLoad = function()
		{
			this.controlHandle.onclick = this.getThis_OnClick();
		}
		
		LinkButton.prototype.getThis_OnClick = function()
		{
			var parentInstance = this;
			
			return (
				function()
				{
					//Raise Event
					ret = parentInstance.onClick(parentInstance, null);
					if (ret != null)
						return ( ret );

					return (false);
				}
			);
		}

		LinkButton.prototype.dispose = function()
		{
		}

//End Class LinkButton