//Class AJAXConnectionManager

	// Constructor *********************************

	AJAXConnectionManager = function()
	{
		this._XMLHttpRequests = new Dictionary();
		this._CallBackFuncs = new Dictionary();
	}
	
	
	// Properties **********************************

	AJAXConnectionManager.prototype._XMLHttpRequests = null;
	AJAXConnectionManager.prototype._CallBackFuncs= null;
	AJAXConnectionManager.prototype._transId = 0;
	
		
	// Methods *************************************

	AJAXConnectionManager.prototype.Execute
		= function(_serviceURL, _params, _callbackfunc)
	{
		this._transId++;
		
		var ac = new AJAXConnection(this._transId,
			this._GetParseResponseHandler(), _serviceURL);
		
		this._XMLHttpRequests.Add(this._transId, ac);
		
		this._CallBackFuncs.Add(this._transId, _callbackfunc);
		
		ac.Execute(_params);
	}
	
	AJAXConnectionManager.prototype.ExecuteSOAP
		= function(_serviceURL, _xmlmessage, _soapAction, _callbackfunc)
	{
		this._transId++;
		
		var ac = new AJAXConnection(this._transId,
			this._GetParseResponseHandler(), _serviceURL);
		this._XMLHttpRequests.Add(this._transId, ac);
		
		this._CallBackFuncs.Add(this._transId, _callbackfunc);

		ac.ExecuteSOAP(_xmlmessage, _soapAction);
	}
	
	AJAXConnectionManager.prototype._GetParseResponseHandler = function()
	{
		var parentInstance = this;
		
		return (
			function(_id, _XMLHttpRequest)
			{
				var func = parentInstance._CallBackFuncs.Get(_id);
				func(_XMLHttpRequest.responseXML);
				
				parentInstance._CallBackFuncs.Remove(_id);
				parentInstance._XMLHttpRequests.Get(_id).Dispose();
				parentInstance._XMLHttpRequests.Remove(_id);
			}
		);
	}
	
	AJAXConnectionManager.prototype.Dispose = function()
	{
		for (var id in this._XMLHttpRequests.Items)
		{
			this._XMLHttpRequests.Get(id).Dispose();
		}		
		this._XMLHttpRequests.Clear();
		this._XMLHttpRequests = null;

		this._CallBackFuncs.Clear();
		this._CallBackFuncs = null;
	}


//End Class AJAXConnectionManager
