 

var ConnectionCommunicationData {
	ActiveSideConnectionID: string;
	ReceivingSideConnectionID: string;
	ProcessRequest: string;
	ProcessParametersString: string;
	ProcessResultString: string;
	CategoryCollectionData: CategoryInfo[];

    constructor() {
					this.ActiveSideConnectionID = ko.observable(this.ActiveSideConnectionID);
			this.ReceivingSideConnectionID = ko.observable(this.ReceivingSideConnectionID);
			this.ProcessRequest = ko.observable(this.ProcessRequest);
			this.ProcessParametersString = ko.observable(this.ProcessParametersString);
			this.ProcessResultString = ko.observable(this.ProcessResultString);
			this.CategoryCollectionData = ko.observableArray(this.CategoryCollectionData);
    }
}

