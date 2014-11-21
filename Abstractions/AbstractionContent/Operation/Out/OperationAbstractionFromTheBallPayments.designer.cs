 

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

		namespace TheBall.Payments { 
				public class FetchCustomersFromStripeParameters 
		{
				public string GroupID ;
				}
		
		public class FetchCustomersFromStripe 
		{
				private static void PrepareParameters(FetchCustomersFromStripeParameters parameters)
		{
					}
				public static void Execute(FetchCustomersFromStripeParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.IContainerOwner Owner = FetchCustomersFromStripeImplementation.GetTarget_Owner(parameters.GroupID);	
				Stripe.StripeCustomer[] StripeCustomers = FetchCustomersFromStripeImplementation.GetTarget_StripeCustomers();	
				CustomerCollection CurrentCustomers = FetchCustomersFromStripeImplementation.GetTarget_CurrentCustomers(Owner);	
				Customer[] NewCustomersToCreate = FetchCustomersFromStripeImplementation.GetTarget_NewCustomersToCreate(Owner, StripeCustomers, CurrentCustomers);	
				FetchCustomersFromStripeImplementation.ExecuteMethod_StoreObjects(Owner, NewCustomersToCreate);		
				}
				}
				public class AssociatePaymentToGroupParameters 
		{
				public string GroupID ;
				public string PaymentID ;
				}
		
		public class AssociatePaymentToGroup 
		{
				private static void PrepareParameters(AssociatePaymentToGroupParameters parameters)
		{
					}
				public static void Execute(AssociatePaymentToGroupParameters parameters)
		{
						PrepareParameters(parameters);
					TheBall.CORE.IContainerOwner GroupAsOwner = AssociatePaymentToGroupImplementation.GetTarget_GroupAsOwner(parameters.GroupID);	
				}
				}
		 } 