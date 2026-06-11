using M2GiantGroupSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class HelpForm_D : Form
    {

        public HelpForm_D()
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        private int clientsSearchIndex = 0;
        private int assetSearchIndex = 0;
        private int jobSearchIndex = 0;
        private int jobRequestIndex = 0;
        private int reportsIndex = 0;
        private int scheduleIndex = 0;
        private int invoiceIndex = 0;
        private int quoteIndex = 0;
        private int staffIndex = 0;
        private int maintenenceIndex = 0;
        private int photosIndex = 0;
        private int mainIndex = 0;
        private int loginIndex = 0;

        private void HelpForm_D_Load(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(100, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            //set all to read only
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
            richTextBox5.ReadOnly = true;
            richTextBox6.ReadOnly = true;
            richTextBox7.ReadOnly = true;
            richTextBox8.ReadOnly = true;
            richTextBox9.ReadOnly = true;
            richTextBox10.ReadOnly = true;

            clientsText.ReadOnly = true;

            //set all to the same font
            richTextBox1.Font = new Font("Segoe UI", 12,FontStyle.Bold);
            richTextBox2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox4.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox5.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox7.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox6.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox8.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox9.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox10.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            clientsText.Font = new Font("Segoe UI", 12, FontStyle.Bold);


          richTextBox6.Text = "ASSETS TAB\r\n\r\nPurpose:\r\nThis tab is used to manage machine asset records. Users can add new owned assets, update existing owned asset details, delete owned asset records, add new hired assets, update existing hired asset details, and delete hired asset records.\r\n\r\n--------------------------------------------------\r\n\r\nADDING AN OWNED ASSET\r\n\r\n1. Open the \"Owned Assets\" tab.\r\n2. Navigate to the Add Asset panel.\r\n3. Enter the asset's:\r\n   • Serial Number\r\n   • Asset Type\r\n   • Purchase Date\r\n   • Current Condition\r\n   • Next Service Date\r\n   • Status\r\n4. Click the \"Add Asset\" button.\r\n5. Confirm the details when prompted.\r\n6. A success message will appear if the asset is added successfully.\r\n\r\nNote:\r\n• All fields are required.\r\n• Serial Number must be entered before submitting.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING AN OWNED ASSET\r\n\r\n1. Click a row in the Owned Asset table to select an asset.\r\n2. The asset's details will load automatically into the fields.\r\n3. Modify the required information.\r\n4. Click \"Update Asset\".\r\n5. Confirm the update when prompted.\r\n6. A success message will appear if the update is successful.\r\n\r\nNote:\r\n• All fields are required before updating.\r\n• You must select an asset from the table before editing.\r\n\r\n--------------------------------------------------\r\n\r\nDELETING AN OWNED ASSET\r\n\r\n1. Navigate to the Delete Asset panel.\r\n2. Begin typing a Serial Number in the search field.\r\n3. The asset's details will load automatically via AutoComplete.\r\n4. Review the loaded details to confirm the correct asset is selected.\r\n5. Click \"Delete Asset\".\r\n6. Confirm the deletion when prompted.\r\n7. A success message will appear if the asset is deleted successfully.\r\n\r\nNote:\r\n• An asset must be loaded before deletion is allowed.\r\n• Deletion is permanent and cannot be undone.\r\n\r\n--------------------------------------------------\r\n\r\nSEARCHING OWNED ASSETS\r\n\r\n1. Type into the Serial Number search field above the Owned Asset table.\r\n2. Matching records will automatically appear in the table.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING OWNED ASSETS\r\n\r\n1. Browse the Owned Asset table on the Owned Assets tab.\r\n2. Click a row to load the asset's full details into the fields.\r\n\r\nStatus Colour Coding:\r\n• Available — Light Green\r\n• In Use — Light Coral (Red)\r\n• Under Maintenance — Light Yellow\r\n\r\nService Date Warning:\r\n• Assets with a service date within 30 days are highlighted in Yellow.\r\n• A warning message will appear on form load listing all affected assets.\r\n\r\n--------------------------------------------------\r\n\r\nHIRED ASSET MODULE\r\n\r\n--------------------------------------------------\r\n\r\nADDING A HIRED ASSET\r\n\r\n1. Open the \"Hired Assets\" tab.\r\n2. Navigate to the Add Hired Asset panel.\r\n3. Enter the asset's:\r\n   • Supplier Name\r\n   • Hire Date\r\n   • Return Date\r\n   • Hire Cost\r\n   • Equipment Type\r\n   • Status\r\n4. Click the \"Add Asset\" button.\r\n5. Confirm the details when prompted.\r\n6. A success message will appear if the asset is added successfully.\r\n\r\nNote:\r\n• All fields are required.\r\n• Hire Cost must be a valid number.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING A HIRED ASSET\r\n\r\n1. Click a row in the Hired Asset table to select an asset.\r\n2. The asset's details will load automatically into the fields.\r\n3. Modify the required information.\r\n4. Click \"Update\".\r\n5. Confirm the update when prompted.\r\n6. A success message will appear if the update is successful.\r\n\r\nNote:\r\n• Supplier Name, Hire Cost, Equipment Type, and Status are all required.\r\n• Hire Cost must be a valid number.\r\n\r\n--------------------------------------------------\r\n\r\nDELETING A HIRED ASSET\r\n\r\n1. Navigate to the Delete Hired Asset panel.\r\n2. Begin typing a Supplier Name in the search field.\r\n3. The asset's details will load automatically via AutoComplete.\r\n4. Review the loaded details to confirm the correct asset is selected.\r\n5. Click \"Delete Asset\".\r\n6. Confirm the deletion when prompted.\r\n7. A success message will appear if the asset is deleted successfully.\r\n\r\nNote:\r\n• A Supplier Name must be entered and an asset loaded before deletion is allowed.\r\n• Deletion is permanent and cannot be undone.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING HIRED ASSETS\r\n\r\n1. Browse the Hired Asset table on the Hired Assets tab.\r\n2. Click a row to load the asset's full details into the fields.\r\n\r\nStatus Colour Coding:\r\n• Active — Light Green\r\n• Returned — Light Coral (Red)\r\n• Overdue — Orange\r\n• Damaged — Light Yellow\r\n\r\nReturn Date Warning:\r\n• Assets with a return date within 7 days are highlighted in Yellow.\r\n• A warning message will appear when switching to the Hired Assets tab listing all affected assets.\r\n\r\n--------------------------------------------------\r\n\r\nIMPORTANT NOTES\r\n\r\n• All required fields must be completed before adding, updating, or deleting a record.\r\n• Hire Cost must be entered as a valid number.\r\n• Changes are only saved after confirmation.\r\n• Deletions are permanent and cannot be reversed.\r\n• Asset details load automatically when selecting a record from the table or typing in a search field.\r\n• Service and return date warnings appear automatically to alert users of upcoming deadlines.\r\n";
          clientsText.Text = "CLIENTS TAB\r\n\r\nPurpose:\r\nThis tab is used to manage client information. Users can add new clients, update existing client details, search for clients, filter client records, archive clients, and reactivate archived clients.\r\n\r\n--------------------------------------------------\r\n\r\nADDING A CLIENT\r\n\r\n1. Open the \"Add Client\" tab.\r\n2. Enter the client's:\r\n   • First Name\r\n   • Surname\r\n   • Email Address\r\n   • Phone Number\r\n   • Client Type\r\n   • Status\r\n3. Click the \"Add Client\" button.\r\n4. Confirm the action when prompted.\r\n5. A success message will appear if the client is added successfully.\r\n\r\nNote:\r\n• Email addresses must be unique.\r\n• South African phone numbers only are accepted.\r\n• All fields are required.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING A CLIENT\r\n\r\n1. Open the \"Update Client\" tab.\r\n2. Select a search criterion (Name, Surname, Email, or Phone).\r\n3. Enter a search value.\r\n4. Select a client from the search results list.\r\n5. The client's details will load automatically.\r\n6. Modify the required information.\r\n7. Click \"Update\".\r\n8. Confirm the update when prompted.\r\n\r\nNote:\r\n• Update controls remain disabled until a client is selected.\r\n• Modified fields are highlighted to indicate changes.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING CLIENTS\r\n\r\n1. Open the \"View Clients\" tab.\r\n2. Browse the client list displayed in the table.\r\n3. Click a row to view detailed client information.\r\n4. The selected client's details will appear in the information panel.\r\n\r\nColour Coding:\r\n• Residential clients - Light Blue\r\n• Commercial clients - Light Cream\r\n• Government clients - Light Green\r\n• Archived clients - Grey\r\n\r\n--------------------------------------------------\r\n\r\nSEARCHING FOR CLIENTS\r\n\r\n1. Select a search category from the Search By dropdown.\r\n2. Enter a search value.\r\n3. Matching records will automatically appear in the table.\r\n\r\nAvailable Search Categories:\r\n• Name\r\n• Surname\r\n• Email\r\n• Phone Number\r\n• Client Type\r\n• Status\r\n• Date Added\r\n\r\n--------------------------------------------------\r\n\r\nFILTERING CLIENTS\r\n\r\n1. Select a Client Type filter.\r\n2. Select a Status filter.\r\n3. The client table will update automatically.\r\n4. Click \"Clear Filters\" to remove all filters.\r\n\r\n--------------------------------------------------\r\n\r\nARCHIVING A CLIENT\r\n\r\n1. Select a client from the client table.\r\n2. Click \"Archive Client\".\r\n3. Confirm the action.\r\n4. The client's status will change to Archived.\r\n\r\nNote:\r\n• Archived clients are not permanently deleted.\r\n• Archived clients can be reactivated later.\r\n\r\n--------------------------------------------------\r\n\r\nREACTIVATING A CLIENT\r\n\r\n1. Select an archived client.\r\n2. Click \"Activate Client\".\r\n3. Confirm the action.\r\n4. The client's status will change back to Active.\r\n\r\n--------------------------------------------------\r\n\r\nIMPORTANT NOTES\r\n\r\n• All required fields must be completed.\r\n• Email addresses must be unique.\r\n• Invalid phone numbers will be rejected.\r\n• Archived clients cannot be archived again.\r\n• Active clients cannot be activated again.\r\n• Changes are only saved after confirmation.";
            richTextBox1.Text = "hi";
            richTextBox2.Text= "QUOTATION MANAGEMENT GUIDE\r\n\r\nPurpose:\r\nThe Quotations form is the central hub for generating, editing, and managing official client job quotes. This module integrates directly with the Job Request system to ensure pricing, scheduling, and documentation are accurate and compliant with company standards.\r\n\r\n---\r\n\r\nCREATING A NEW QUOTE\r\n\r\n1. Select a Job Request:\r\n   • Choose the relevant client job request from the management grid.\r\n\r\n2. Add Services:\r\n   • Double-click items from the \"Job Type\" list to add them to your active quote.\r\n\r\n3. Adjust Quantities:\r\n   • Modify the QTY column in the \"Selected Jobs\" grid.\r\n   • The system will automatically calculate line totals and the grand total (including VAT).\r\n\r\n4. Save Quote:\r\n   • Once all items are added and the details are verified, click the \"Save\" button to commit the quote to the database.\r\n\r\n---\r\n\r\nMANAGING QUOTE DOCUMENTATION\r\n\r\nOnce a quote is active, the following actions are available:\r\n\r\n• Save as PDF:\r\nClick to export the quote into a professional PDF format.\r\n\r\nNote:\r\n• This locks the storage path to the database to ensure the document remains correctly linked to the record.\r\n\r\n• Print Quote:\r\nTriggers the system print dialog to produce a physical copy for the client.\r\n\r\n• Delete Quote:\r\nRemoves the record from the system.\r\n\r\nNote:\r\n• This action is permission-restricted.\r\n• If the button is disabled, please contact an Administrator.\r\n\r\n---\r\n\r\nSECURITY & COMPLIANCE\r\n\r\n• Anti-Fraud Guard Rails:\r\nThe system prevents the backdating or future-dating of \"Issued\" or \"Generated\" dates.\r\n\r\n• Expiry Logic:\r\nThe system will reject expiry dates set in the past or dates that occur before the Issue Date.\r\n\r\n• Access Control:\r\nAll functions are role-dependent.\r\nSensitive actions like deleting records are restricted to authorized personnel.\r\n\r\n---\r\n\r\nAUTOMATED CALCULATIONS\r\n\r\n• Travel Fee Logic:\r\nThe system calculates transport costs using the Haversine formula.\r\nIt computes the distance between your base office and the job site, adding a Base Call-Out Fee (R450.00) and a Per-Kilometer Rate (R6.50/km).\r\nIf location coordinates are missing, a flat fallback fee of R850.00 is applied.\r\n\r\n• VAT & Totals:\r\nThe system is pre-configured with a 15% VAT rate.\r\nWe separate travel fees (if applicable) before calculating VAT on the service line items, ensuring total transparency in your financial summary.\r\n\r\n---\r\n\r\nIMPORTANT NOTES\r\n\r\n• Validation:\r\nAlways ensure a valid \"Quote Status\" is selected before saving.\r\n\r\n• Resetting:\r\nUse the \"Clear\" button to reset the form for a new entry.\r\nThis clears all selections, calculations, and data grids, ensuring no data from previous quotes carries over.\r\n\r\n• Theme Management:\r\nThe form automatically adapts to your system's light or dark mode preferences for optimal visibility.\r\n\r\n• All quote calculations are performed automatically by the system.\r\n• Travel fees and VAT are calculated according to predefined business rules.\r\n• Quote deletion is restricted to authorized users.\r\n• Saved PDF documents remain linked to the corresponding quote record.\r\n\r\n---\r\n\r\nSUPPORT\r\n\r\nFor further technical support regarding VAT adjustments or mileage rates, please consult your System Administrator.\r\n";
            richTextBox3.Text = "JOB ALLOCATION AND ASSET MANAGEMENT FORM\r\n\r\nPurpose:\r\nThis form is used to assign staff members and assets (both owned and hired) to specific jobs. Users can view available resources, manage current allocations for active jobs, and update job assignments.\r\n\r\n---\r\n\r\nSELECTING A JOB\r\n\r\n1. Open the form.\r\n\r\n2. If a job was selected via the Calendar, its details will load automatically.\r\n\r\n3. If no job is selected, click \"Load Jobs\" to see all jobs currently in progress.\r\n\r\n4. Select a job from the main table by double-clicking the row.\r\n\r\n5. Job details (ID, Client Name, Site Address, and Status) will populate in the header boxes.\r\n\r\nNote:\r\n• You can also click the \"Open Calendar\" button to browse and select a different job from the scheduling calendar.\r\n\r\n---\r\n\r\nASSIGNING ASSETS\r\n\r\n1. Select a job from the list.\r\n\r\n2. Choose an asset from either the \"Owned Assets\" or \"Hired Assets\" tab.\r\n\r\n3. Click the \"Assign Asset\" button.\r\n\r\n4. Confirm the action when prompted.\r\n\r\n5. The asset will move to the \"Job Asset Assignment\" grid.\r\n\r\n---\r\n\r\nREMOVING ASSETS\r\n\r\n1. In the \"Job Asset Assignment\" grid, select the assigned asset you wish to remove.\r\n\r\n2. Click the \"Remove Asset\" button.\r\n\r\n3. Confirm the deletion to release the asset back to the available pool.\r\n\r\n---\r\n\r\nASSIGNING STAFF\r\n\r\n1. Select a job.\r\n\r\n2. Select a staff member from the \"Available Staff\" grid.\r\n\r\n3. Click the \"Assign Staff\" button.\r\n\r\n4. Confirm the assignment.\r\n\r\n5. The staff member will appear in the \"Job Staff Assignment\" grid.\r\n\r\nNote:\r\n• Top Management and Admin-level staff members are restricted and cannot be assigned to operational jobs.\r\n\r\n---\r\n\r\nREMOVING STAFF\r\n\r\n1. Select the staff assignment row from the \"Job Staff Assignment\" grid (right-hand grid).\r\n\r\n2. Click the \"Remove Staff\" button.\r\n\r\n3. Confirm the removal.\r\n\r\n---\r\n\r\nSEARCHING FOR JOBS\r\n\r\n1. Use the \"Search\" text box located near the job list.\r\n\r\n2. Type any keyword (e.g., Client Name or Status).\r\n\r\n3. The table will filter results in real-time as you type.\r\n\r\n4. Clear the text box to show all \"In Progress\" jobs again.\r\n\r\n---\r\n\r\nIMPORTANT NOTES\r\n\r\n• All assignments require a job to be selected first.\r\n\r\n• Ensure the correct asset list (Owned vs. Hired) is viewed before assigning.\r\n\r\n• Changes to assignments are updated immediately in the database upon confirmation.\r\n\r\n• If you do not see a resource in the available lists, check if it has already been assigned to another task.\r\n\r\n• Job details populate automatically when a valid job is selected.\r\n\r\n• Assigned assets are moved from the available list to the Job Asset Assignment grid.\r\n\r\n• Assigned staff are moved to the Job Staff Assignment grid once the assignment is confirmed.\r\n\r\n---\r\n\r\nSUPPORT\r\n\r\nIf resources are missing from the available lists or assignments cannot be completed, verify that the resource has not already been allocated to another active job and contact your System Administrator if the issue persists.\r\n";
            richTextBox4.Text = "hi";
            richTextBox5.Text = "hi";
            richTextBox7.Text = "hi";
            
            richTextBox8.Text = "STAFF MANAGEMENT GUIDE\r\n\r\nPurpose:\r\nThe Staff Management form is the central interface for viewing, adding, and updating staff records. This module ensures that administrative, operational, and staff data is managed securely according to authorized user access levels.\r\n\r\n---\r\n\r\nNAVIGATING THE INTERFACE\r\n\r\nThe form is organized into two primary tabs, controlled by the system's security settings:\r\n\r\n• View/Edit Tab:\r\nUsed for selecting existing staff members and updating their details.\r\nClicking any row in the DataGridView will automatically populate the fields in the \"Edit Staff\" panel.\r\n\r\n• Add Staff Tab:\r\nUsed for creating new records.\r\nFill in the required fields and select the appropriate Role and Status from the provided dropdowns before clicking \"Add Staff.\"\r\n\r\n---\r\n\r\nSECURITY & PERMISSIONS\r\n\r\nThe system enforces strict Access Control to protect sensitive payroll and status information. Permissions are determined by your assigned User Level:\r\n\r\n• Level 6 (Owner)\r\nRole: Owner\r\nCapabilities:\r\n• Full access to add, edit, and modify all staff fields.\r\n\r\n• Level 5 (Admin)\r\nRole: Admin\r\nCapabilities:\r\n• Can edit existing staff.\r\n• Cannot add new staff or change rates/status.\r\n\r\n• Level 4 (Ops Manager)\r\nRole: Ops Manager\r\nCapabilities:\r\n• Can view staff.\r\n• Cannot add, change roles, modify daily rates, or change status.\r\n\r\n• Level 3 and Below\r\nRole: Standard User\r\nCapabilities:\r\n• View-only access.\r\n• Restricted from modifying staff records.\r\n\r\nNote:\r\n• If specific buttons or fields are disabled (greyed out), your current User Access Level does not grant you the authority to modify those settings.\r\n\r\n---\r\n\r\nKEY FUNCTIONS\r\n\r\nSEARCHING STAFF\r\n\r\n1. Use the \"Search\" field.\r\n2. Filter records by First Name or Last Name.\r\n3. Results refresh automatically as you type.\r\n\r\n---\r\n\r\nSAVING CHANGES\r\n\r\n1. Update the required details in the \"Edit\" fields.\r\n2. Click \"Save\".\r\n3. The system will validate your input before committing changes to the database.\r\n\r\nValidation Checks:\r\n• A valid Staff ID must be selected.\r\n• A Role must be selected.\r\n• Required fields must be completed.\r\n\r\n---\r\n\r\nDATA VALIDATION\r\n\r\nThe system will display a warning message if you attempt to save an incomplete record.\r\n\r\nExamples include:\r\n• Missing Status\r\n• Invalid Staff ID\r\n• Missing Role\r\n\r\n---\r\n\r\nOPERATIONAL MAINTENANCE\r\n\r\nAUTOMATIC REFRESH\r\n\r\n• After adding or updating a staff member, the system automatically refreshes the relevant data grids to display the most current information.\r\n\r\n---\r\n\r\nCLEAR FIELDS\r\n\r\n• The form includes a \"Clear\" function for both Add and Edit panels.\r\n• This resets all text boxes and dropdowns.\r\n• Prevents data from previous operations from being carried over accidentally.\r\n\r\n---\r\n\r\nTHEME INTEGRATION\r\n\r\n• The interface automatically switches between Light and Dark modes to match your global system theme settings.\r\n\r\n---\r\n\r\nTROUBLESHOOTING\r\n\r\n• \"No roles found\"\r\nEnsure your database connection is active.\r\n\r\n• \"Update failed\"\r\nVerify that all mandatory fields (specifically Status and Role) are selected.\r\nIf the error persists, contact your System Administrator.\r\n\r\n---\r\n\r\nIMPORTANT NOTES\r\n\r\n• Staff permissions are controlled by User Access Levels.\r\n• Some controls may be disabled depending on your assigned role.\r\n• All mandatory fields must be completed before records can be saved.\r\n• Data grids refresh automatically after successful additions or updates.\r\n• Use the Clear function before starting a new operation to avoid accidental data entry errors.\r\n• Pay rate and role modifications require appropriate Owner-level permissions.\r\n\r\n---\r\n\r\nSUPPORT\r\n\r\nFor any changes to staff pay rates or role definitions, please ensure you have the appropriate \"Owner\" level permissions.\r\n";
            richTextBox9.Text = "hi";
            richTextBox10.Text = "hi";
            richTextBox11.Text = "MAIN MENU FORM\r\n\r\nPurpose:\r\nThe Main Menu serves as the central hub of the application. From here, users can navigate to all functional areas of the system, manage system settings, and access support documentation.\r\n\r\n---\r\n\r\nNAVIGATING THE SYSTEM\r\n\r\n1. Use the top Menu Strip to browse categories (e.g., Clients, Jobs, Assets, Quotes, Reports).\r\n2. Click on a specific task (e.g., \"Add New Client\" or \"Capture Job Request\") to open the relevant form.\r\n3. Once a form is opened, it will appear as an active sub-window.\r\n4. Use the \"Exit to Menu\" button to close the current sub-window and return to the main dashboard.\r\n\r\nNote:\r\n• The \"Exit to Menu\" button is disabled when no sub-windows are open to prevent unnecessary clicks.\r\n\r\n---\r\n\r\nACCESS CONTROL (PERMISSIONS)\r\n\r\nThe system automatically restricts access based on your user role level:\r\n\r\n• Owner (Level 6): Full access to all menus and features.\r\n• Admin (Level 5): Restricted access to specific sensitive management tools.\r\n• Ops Manager (Level 4): Limited access; core management and setup tools are disabled.\r\n• General User (Level 3 & below): Lockdown mode; most menu items are disabled for security.\r\n\r\n---\r\n\r\nFUNCTIONAL AREAS\r\n\r\n• Clients: Add, update, or view client profiles.\r\n• Jobs: Capture new requests, add job details, update progress, and view site photos.\r\n• Assets: Manage owned machines, hired equipment, and maintenance logs.\r\n• Allocations: Allocate staff and assets to jobs and view the scheduling calendar.\r\n• Quotes & Invoicing: Create, edit, and print professional job quotes and invoices.\r\n• Reports: View system-generated reports for business analysis.\r\n\r\n---\r\n\r\nSYSTEM CONTROLS\r\n\r\n• Theme Management: The system automatically applies light/dark mode settings based on your preferences.\r\n• Session Management: Click \"Logout\" (or the relevant close-session menu item) to securely sign out and return to the Login screen.\r\n\r\n---\r\n\r\nIMPORTANT NOTES\r\n\r\n• Active Sub-Windows: Only one functional sub-form can be active at a time to ensure data integrity and focus.\r\n• Auto-Permissions: Menu items will automatically gray out (disable) if your current user session does not have the required access level.\r\n• Navigation: Always use the \"Exit to Menu\" button to close active tasks; this ensures the system state is cleaned up properly.\r\n• Support: If you require assistance, click the \"Help\" menu item to access the documentation guide.\r\n";
            richTextBox12.Text = "USER ACCESS & LOGIN GUIDE\r\n\r\nPurpose:\r\nThis guide outlines the standard procedures for accessing the M2 Giant Group System and the secure process for recovering your account if you forget your password.\r\n\r\n---\r\n\r\nACCESSING THE SYSTEM\r\n\r\nTo maintain a secure environment, the login process requires valid credentials verified against our encrypted database.\r\n\r\nSteps to Login:\r\n\r\n1. Enter Credentials:\r\n   • Input your assigned Username and Password into the respective text boxes.\r\n\r\n2. View Password:\r\n   • If you wish to verify your input, check the \"Show Password\" box to toggle visibility.\r\n\r\n3. Authentication:\r\n   • Click \"LOGIN\".\r\n   • Note: The system will temporarily disable the button and show an \"Authenticating...\" status to prevent multiple submissions while the connection is established.\r\n\r\n4. Successful Access:\r\n   • Upon validation, you will be greeted with a welcome message and directed to the Main Menu.\r\n\r\n---\r\n\r\nPASSWORD RECOVERY (FORGOT PASSWORD)\r\n\r\nIf you cannot remember your login credentials, the system provides a secure multi-step restoration process.\r\n\r\nThe Reset Workflow:\r\n\r\n1. Initiate Request:\r\n   • Click the \"Forgot Password?\" link on the login screen.\r\n\r\n2. Email Verification:\r\n   • Enter your registered email address.\r\n   • The system will verify this against our database to ensure it matches an existing account.\r\n\r\n3. Receive OTP:\r\n   • A One-Time Password (OTP) will be sent to your email address.\r\n\r\n   Tip:\r\n   • If you do not see the email in your inbox within a few minutes, please check your Spam or Junk folders.\r\n\r\n4. Verify OTP:\r\n   • Enter the OTP into the verification prompt.\r\n   • Once the code is validated, you will be granted access to the reset page.\r\n\r\n5. Reset Password:\r\n   • You will be prompted to create a new, secure password that complies with our complexity requirements.\r\n\r\n6. Finalize:\r\n   • After saving, you may log in to the system using your new credentials.\r\n\r\n---\r\n\r\nSECURITY PROTOCOLS\r\n\r\n• Security Through Obscurity:\r\nIn the event of a failed login attempt, the system returns a generic \"Invalid username or password\" error. This prevents unauthorized individuals from determining which part of your credentials was incorrect, stopping \"username harvesting.\"\r\n\r\n• Advanced Encryption:\r\nWe utilize BCrypt hashing for all passwords. Your raw password is never stored; it is converted into a secure, one-way hash, ensuring your account remains protected.\r\n\r\n---\r\n\r\nTROUBLESHOOTING\r\n\r\n• Connection Errors:\r\nIf a \"Database Error\" occurs, please verify your network connection.\r\n\r\n• Invalid Credentials:\r\nPlease double-check for typos. If the issue persists, use the \"Forgot Password\" link rather than attempting to guess your password, as frequent failed attempts may trigger a temporary account lock.\r\n\r\n• Exit:\r\nIf you decide not to proceed, click the \"Exit\" button to close the application.\r\n\r\n---\r\n\r\nIMPORTANT NOTES\r\n\r\n• Valid login credentials are required to access the system.\r\n• OTP codes are sent only to registered email addresses.\r\n• Check your Spam or Junk folder if the OTP email is not received.\r\n• Passwords are securely stored using BCrypt hashing.\r\n• Repeated failed login attempts may result in a temporary account lock.\r\n• Contact your System Administrator if you require further assistance regarding account access or role permissions.\r\n";

        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.SetDarkMode(!ThemeManager.IsDarkMode);
            UpdateToggleLabel();
        }

        private void trkFontSize_Scroll(object sender, EventArgs e)
        {
           
        }

        private void UpdateToggleLabel()
        {
           
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }

        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void lblFontSize_Click(object sender, EventArgs e)
        {

        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 11, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            Color textColor = Color.Black;

            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.DarkGreen;
                textColor = Color.White;
            }

            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void HighlightAll(string searchText, RichTextBox rb)
        {
            rb.SelectAll();

            
            rb.SelectionBackColor = Color.FromArgb(251, 253, 223); ;
           

            if (string.IsNullOrWhiteSpace(searchText))
                return;

            int start = 0;

            while ((start = rb.Text.IndexOf(
                searchText,
                start,
                StringComparison.OrdinalIgnoreCase)) != -1)
            {
                rb.Select(start, searchText.Length);
                rb.SelectionBackColor = Color.Yellow;
                start += searchText.Length;
            }

            
        }

        private void seacrchClients_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(seacrchClients.Text, clientsText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(clientsText, seacrchClients.Text, ref clientsSearchIndex);
        }
        private void FindNextInRichTextBox(
    RichTextBox rtb,
    string searchText,
    ref int lastIndex)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return;

            int index = rtb.Find(searchText, lastIndex, RichTextBoxFinds.None);

            if (index == -1)
            {
                lastIndex = 0;
                index = rtb.Find(searchText, lastIndex, RichTextBoxFinds.None);
            }

            if (index != -1)
            {
                rtb.Select(index, searchText.Length);
                rtb.ScrollToCaret();
                rtb.Focus();

                lastIndex = index + searchText.Length;
            }
            else
            {
                MessageBox.Show("Text not found.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox1.Text, richTextBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox1, textBox1.Text, ref jobRequestIndex);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox2.Text, richTextBox2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox2, textBox2.Text, ref quoteIndex);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox3.Text, richTextBox3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox3, textBox3.Text, ref scheduleIndex);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox4.Text, richTextBox4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox4, textBox4.Text, ref jobSearchIndex);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox5.Text, richTextBox5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox5, textBox5.Text, ref invoiceIndex);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox6.Text, richTextBox6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox6, textBox6.Text, ref assetSearchIndex);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox7.Text, richTextBox7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox7, textBox7.Text, ref maintenenceIndex);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox8.Text, richTextBox8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox8, textBox8.Text, ref staffIndex);
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox9.Text, richTextBox9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox9, textBox9.Text, ref reportsIndex);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox10.Text, richTextBox10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox10, textBox10.Text, ref photosIndex);
        }

        private void seacrchClients_Enter(object sender, EventArgs e)
        {
            seacrchClients.Clear();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            textBox9.Clear();
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            textBox10.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set all textboxes back to Search...
            textBox1.Text = "Search...";
            textBox2.Text = "Search...";
            textBox3.Text = "Search...";
            textBox4.Text = "Search...";
            textBox5.Text = "Search...";
            textBox6.Text = "Search...";
            textBox7.Text = "Search...";
            textBox8.Text = "Search...";
            textBox9.Text = "Search...";
            textBox10.Text = "Search...";
            textBox11.Text = "Search...";
            textBox12.Text = "Search...";


            seacrchClients.Text = "Search...";
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox11.Text, richTextBox11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox11, textBox11.Text, ref mainIndex);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox12.Text, richTextBox12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox12, textBox12.Text, ref loginIndex);
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {
            textBox11.Clear();
        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            textBox12.Clear();
        }
    }
}



