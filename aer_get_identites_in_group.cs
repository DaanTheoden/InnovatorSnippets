//****************************************************************************************
// [AUTHOR]: SOFTWERK: D. Theoden
// [CREATED ON]: 2024-01-18
// [METHOD]: aer_get_identites_in_group.cs
// ---------------------------------------------------------------------------------------
// [DESCRIPTION]:
// Generic, reusable code to get all users in a group.
// ---------------------------------------------------------------------------------------
// [CHANGES]:
// REF NO       DATE            WHO             DETAIL
// #001         2024-01-22      D. Theoden      Initial version.
//****************************************************************************************

// Define parameters
Item item = this;
Innovator inn = item.getInnovator();
List<string> userIdentities = new List<string>();

Item initialIdentity = inn.getItemById("Identity", "804E11705E1D4E98AEBBDB8E9AFC0B7C"); // Your existing logic to get a (group) identity
ProcessIdentity(initialIdentity, inn, userIdentities);

// Rest of your logic here. All recursively found identities are now stored in userIdentites array. 
return this;

} // Allow functions in server methods Part A: extra curly brace. This is NOT a mistake.

// Function to recursively process identities and add unique users to the list
void ProcessIdentity(Item identity, Innovator inn, List<string> userIdentities) {
    if (identity.getProperty("is_alias", "") == "1") {
        // Add user identity if it's unique
        string userId = identity.getID();
        if (!userIdentities.Contains(userId)) {
            userIdentities.Add(userId);
        }
    } else {
        // Process group members
        Item groupMembers = inn.applyAML("<AML><Item type='Member' action='get' select='related_id'><related_id><Item type='Identity' action='get'></Item></related_id><source_id>" + identity.getID() + "</source_id></Item></AML>");
        int memberCount = groupMembers.getItemCount();
        for (int j = 0; j < memberCount; j++) {
            Item member = groupMembers.getItemByIndex(j);
            Item memberIdentity = member.getRelatedItem();
            ProcessIdentity(memberIdentity, inn, userIdentities);
        }
    }
}

// Any more functions of your own...

void enableFunctionsInMethods() { // Allow functions in server methods Part B: opening dummy method. This is NOT a mistake.
