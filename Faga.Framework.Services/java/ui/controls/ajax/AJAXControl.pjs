//Class AJAXControl

	// Constructor *********************************
	
	/* abstract class */
		AJAXControl = function() {}
	
	
	// Inheritance *********************************
	
		AJAXControl.prototype = new Control();

	
	// Properties **********************************

		AJAXControl.prototype._baseServiceURL = "http://{SERVER}/wal/";
		//AJAXControl.prototype._baseServiceURL = "http://localhost:8088/wal/";
		AJAXControl.prototype._serviceURL = null;
		AJAXControl.prototype._params = null;
		AJAXControl.prototype._xmlmessage = null;
		AJAXControl.prototype._soapAction = null;
		
		
	// Methods *************************************

		AJAXControl.prototype.Execute = function(_serviceURL, _params)
		{
			if (_serviceURL != null) this._serviceURL = _serviceURL;
			if (_params != null) this._params = _params;
			
			this.page.ACM.Execute( this._serviceURL, this._params,
				 this._GetParseResponseHandler() );
		}

		
		AJAXControl.prototype.ExecuteSOAP = function(_serviceURL, _xmlmessage, _soapAction)
		{
			if (_serviceURL != null) this._serviceURL = _serviceURL;
			if (_xmlmessage != null) this._xmlmessage = _xmlmessage;
			if (_soapAction != null) this._soapAction = _soapAction;

			this.page.ACM.ExecuteSOAP( this._serviceURL, this._xmlmessage,
				this._soapAction, this._GetParseResponseHandler() );
		}
		
		
		AJAXControl.prototype._GetParseResponseHandler = function()
		{
			var parentInstance = this;
			
			return (
				function(_responseXML)
				{
					parentInstance.ParseResponse(_responseXML);
				}
			);
		}
			
		/* must implement */
		AJAXControl.prototype.ParseResponse = function(_responseXML) {}

		/* must implement */
		AJAXControl.prototype.onLoad = function() {}

		/* must implement */
		AJAXControl.prototype.dispose = function() {}
		
		
		AJAXControl.prototype.toString = function()
		{
			return ("AJAXControl object");
		}


//End Class AJAXControl