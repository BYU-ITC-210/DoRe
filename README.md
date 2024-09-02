# DNS for IT Learning

This is a DNS server to that allows students to create their own DNS records to support programming projects. Records they create are subdomains on the main domain of the DNS service. For example, if the service manages the domain `example.com` then students could create domain records for `allyson.example.com` or `charlie.example.com`.

This is a serverless application running on Azure Functions. Accordingly, it's very inexpensive to host. For the original class at Brigham Young University it cost less than $1 a month for hosting. DNS registration charges are in addition to the hosting cost.

## Build and Deploy.

There is not yet an automated deployment script - deployment must be done by hand. While these instructions may look long. It's really pretty easy. We just went to extra detail on them.

1. Register a domain to be used by your class. You can use any domain registrar that lets you set custom name servers (which is pretty much any registrar). The initial deployment uses NameCheap.com
2. Create a Microsoft Azure account if you do not have one yet. See Azure documentation for the various billing options. The initial deployment uses a pay-as-you-go subscription billed to a credit card.
3. On the Azure portal, create a `DNS Zone` (not a Private DNS zone and not a DNS private resolver).
    1. Follow the instructions to name the zone the same as the zone registered in step 1.
    2. In your domain registrar (from step 1) set the nameservers to the values provided by the Azure DNS service.
4. On the Azure portal, create a `Function App`.
    1. The default domain name of the server will be on the ".azurewebsites.net" domain.  E.g. "phred.azurewebsites.net". Give it a name that describes its use. E.g. "dnsforexample.azurewebsites.net". Later, you will be able to give the server a custom domain name.
    2. Runtime stack is ".NET"
    3. Version is "6 (LTS)"
    4. Region should be the same as other resources you host on Azure but it doesn't matter much.
    5. Operating System is "Windows"
    6. Hosting plan is "Consumption (Serverless)"
    7. Storage Account: Recommended to create a new storage account and name it the same as the function application. However any existing or new storage account should work.
    8. Enable public access: "On"
    9. Enable network injection: "Off"
    10. Enable Application Insights: "No" (Just to begin with. You can add application insights later.)
    11. Continuous Deployment: "Disable"
    12. Basic Authentication: "Enable"
    12. Tags: Don't add any at present though you will add some later.
    13. Wait for deployment to complete.
5. Deploy your function app to the Azure cloud
    1. In Visual Studio open the DnsForItLearningLabs solution.
    2. In the menu, pick **Build > Publish Selection**
    3. Create a new publish profile.
    4. Select **Azure** and the publish destination.
    5. Select **Azure Function App (Windows)** as the "specific target"
    6. On the Publish step make sure the right Microsoft account is selected in the upper-right. It should be the same account you used to create the Function App on the Azure portal.
    7. Select the correct Azure subscription.
    8. Select the function app you created in step 4 and click **Next**.
    9. Pick **Publish (generates pubxml file)** and click **Finish**.
    10. Wait until the publish profile is created and close the profile creation wizard.
    11. Select the profile you just created and click **Publish**.
    12. Wait until the application publish operation is complete.
6. Grant the **Function App** access to your **DNS Zone**
    1. In the Azure Portal select the **Function App** you created in step 4.
    2. Select **Settings > Identity** in the left column.
    3. In the **System assigned** tab set **Status** to **On** and click **Save**.
    4. When it asks, "Enable system assigned managed identity" select **Yes**.
    5. In the Azure Portal select the **DNS Zone** you created in step 3.
    6. Select **Access Control (IAM)** in the left column.
    7. Click **+ Add** at the top of the page and choose **Add role assignment**
    8. From the list of roles, choose **DNS Zone Contributor** and click **Next**
    9. Set **Assign access to** to "Managed identity" and click **+ Select members**
    7. Subscription should be correct.
    8. Managed Identity is **Function App**
    9. Select the function app created in step 4 and click **Select**
    10. Click **Review + assign**
    11. Verify the assigned access by clicking the **Role assignments** tab.
7. Configure your **Function App**
    1. In the Azure Portal access your **Function App**
    2. Select **Settings > Environment variables** in the left column.
    3. In the **App Settings** tab click **+ Add**
    4. Set **Name** to **dns_zone** and value to the domain name you registered in step 1. E.g. "example.com"
    5. Click **OK** and then click **Apply** at the bottom of the screen to save the setting.
8. Configure an admin account and test the application.<br/>*Technically, an admin account isn't required. The application will function with just LTI authentication from your LMS. But, having an admin account is helpful for testing and diagnostics.*
    1. Browse to https://&lt;yourAppName&gt;.azurewebsites.net/util. Use the name of the **Function App** upi created in step 4. Make sure you include "/util" for the path.
    2. Under **Hash Password**, select "admin" for the account type, choose a username and password, and click **Submit**.
    3. It will respond with the name and value for a tag to set on your DNS service. The name will be "user_" followed by the username you selected. The value will be "admin;" followed by a secure password hash. Keep this page open while you do the next steps in another browser tab.
    4. In the Azure Portal access your **DNS Zone**. (Make sure this is the DNS zone and not your Functions App. Both have tags but the admin account tag will only work on the DNS Zone).
    5. In the left column click **Tags**.
    6. In the open boxes fill in a new tag with the name and value produced by *Hash Password* in the previous step.
    7. Click **Apply** at the bottom of the page.
    8. You can create more admin and/or user accounts by following the prior steps up to the limit of 50 tags. However, the application is designed to use LTI for authentication with an unlimited number of users. These accounts are only for administration and testing.
9. Test the DNS service.
    1. Browse to https://&lt;yourAppName&gt;.azurewebsites.net/c/login and log in using the admin username and password you created with *Hash Password* above.
    2. You will see the main domain screen. Test the functionality by claiming a subdomain and setting an A record on that domain.
    3. See if the domain record is live by going to a command line and typing `nslookup &lt;theSubDomain&gt;`. For example `nslookup mydomain.example.com`.
10. Give your service a custom domain name and create a TLS certificate for the domain.
    1. In the Azure Portal, access your **Functions App**.
    2. Select **Custom domains** in the left column.
    3. Click **+ Add custom domain**.
    4. For **Domain Provider** select "All other domain services."
    5. For **TLS/SSL certificate** select "Add certificate later"
    6. For **Domain** select the domain name for the service. We recommend "dns" plus the domain name you registered in step 1. For example, "dns.example.com". Regardless, if you want to have Azure manage your TLS certificate and automatically renew it (without charge) the domain name server MUST be an Azure DNS Zone.
    7. Hostname record type is automatically set to CNAME.
    8. Follow the instructions under Domain validation to set the DNS records on your name server as described. Do this in another browser tab while keeping the current tab open.
    9. Click **Validate** at the bottom of the page.
    10. Once validation passes, click **Add** at the bottom of the page.
    11. The page will have a warning, "One or more domains are not secured..."
    12. Click **Add binding** next to your newly added domain.
    13. **Add new certificate** should already be selected.
    14. For **TLS/SSL type** select "SNI SSL"
    15. For **Source** select "Create App Service Managed Certificate".
    16. Click **Validate** at the bottom of the screen.
    17. Once validation passes, click **Add**.
    18. Note should say, "It may take up to 10 minutes for a certificate to be issued.
    19. Once the certificate has been issued, test the custom domain by browsing to "https://&lt;yourCustomDomain&gt;/c/login". For example, "https://dns.example.com/c/login".
11. Connect your DNS to your Learning Management System (LMS) using LTI.
    1. Browse to the util page on your Functions App. This is the domain you used in the previous step. For example, "https://dns.example.com/util.
    2. Under **Generate Key for LTI Connection** type a name for the key. Typically this is the name of your LMS or the name of your course within the LMS.
    3. In the Azure Portal, add the specified tag and value as a new tag on the **DNS Zone**. (Make sure you set the tag on the DNS Zone and not on the Function App). Click **Apply**.
    4. Follow your LMS instructions on how to add an LTI link. You will need the URL, Key, and secret that were provided in step 2.
    5. Click the link and you should go directly to the DNS management page. Claim a domain and add records.

That's it!

## Running and Debugging Locally

To run and debug the application on your local machine, it will credentials to access the the Azure DNS Zone. This takes the place of step 6 above when the application is running on Azure.

1. Create an App Registration
    1. In the Azure Portal go in to **Azure Active Directory**.
    2. Make note of the **Primary domain** as you will need it later. It is typically something like "example.onmicrosoft.com". 
    2. Select **App registrations** on the left.
    3. Create a **New registration** and name it "DnsDevelopment" or something descriptive.
    4. Set it as "Single Tenant".
    5. Leave the Redirect URI blank - it's not used for this applicaiton.
    6. Click Register.
    7. On the new page, copy the application (client) ID.
    8. Click **Add a certificate or secret**
    9. Click **+New client secret**
    10. Give it a description like "Local Development".
    11. Set the expiration which can be up to 24 months.
    11. Click "Add".
    12. Copy the **Secret Value** and store it with the **Client ID** somewhere secure such as in a password database. This will be the only time you can access the **Secret Value**. You will not need the **Secret ID** though you can look it up at any time.
2. Grant the App Registration access to you DNS Zone
    1. In the Azure Portal go to your DNS Zone
    2. Select **Access control (IAM)**
    3. Click **+Add** and select **Add role assignment**
    4. From the list of roles, choose **DNS Zone Contributor** and click **Next**
    5. Set **Assign access to** to "User, group or service principal" and click **+ Select members**
    6. Subscription should be correct.
    7. Managed Identity is **Function App**
    8. Select the function app created in step 4 and click **Select**
    9. Type the name of the App Registration you used. Until you start typing it won't show as something to select.
    10. Click the correct App Registration and click **Select**.
    11. Click **Review and Assign**
    11. Back in the Access Control (IAM) page click **Role assignments** to view and verify that the assignment was made.
3. Add the access credentials to your local configuration.
    1. Open "local.settings.json" in your project.
    2. Make sure "local.settings.json" is listed in .gitignore. Since you will be storing secrets in the file it **MUST NOT** be committed to your repo. Use **sample.local.settings.json** to show an example without any secrets.
    3. In "local.settings.json" set **AZURE_TENANT_ID** the name of your active directory that you noted back in step 1.
    4. Set **AZURE_CLIENT_ID** to the ID of your App Registration that you created in step 1.
    5. Set **AZURE_CLIENT_SECRET** to the client secret you created in step 1 and saved.

The application uses DefaultAzureCredential to access the DNS Zone API. In this case, DefaultAzureCredential is enabled to try EnvironmentCredential, and ManagedIdentityCredential. EvironmentCredential uses the three settings, AZURE_TENANT_ID, AZURE_CLIENT_ID, and AZURE_CLIENT_SECRET. ManagedIdentityCredential uses the direct assignment of permissions to the Azure Functions App. Note that the secret in EnvironmentCredential will expire while ManagedIdentityCredential has perpetual access.

## Updating the UX

The `DnsForItLearningLabsUI` folder contains the UI which is written in Vue. That folder should be in the same parent folder as this project, `DnsForLearningLabs`. That is, in one folder you should find these two subfolders.

1. Open a console and change directories to `DnsForLearningLabsUI`.
2. Type the command, `npm run build`.
3. The files in this project, under the `\c` folder should be updated with the latest UI.
4. In Visual Studio, make sure all files in the `\c` folder are set to "Copy to output directory if newer." Do this by right right-clicking the file, selecting `Properties` setting `Build Action` to "None" and setting `Copy to output directory` to "Copy if newer." This should only be necessary for files that were changed or added.
5. Rebuild and publish to Azure.
