document.addEventListener("DOMContentLoaded", async () => {
    const announcementImage = document.getElementById("announcementImage");
    const announcementTitle = document.getElementById("announcementTitle");
    const announcementDescription = document.getElementById("announcementDescription");
    const announcementPhone = document.getElementById("announcementPhone");
    const backBtn = document.getElementById("backBtn");

    // Get announcement ID from query string
    const urlParams = new URLSearchParams(window.location.search);
    const announcementId = urlParams.get("id");

    if (!announcementId) {
        alert("Announcement ID is missing");
        return;
    }

    // Fetch Announcement Details
    async function fetchAnnouncementDetails(id) {
        try {
            const response = await fetch(`/api/announcements/${id}`);
            if (!response.ok) {
                throw new Error("Failed to fetch announcement details");
            }

            const announcement = await response.json();
            // Populate the fields with announcement data
            announcementImage.src = announcement.image;
            announcementTitle.textContent = announcement.title;
            announcementDescription.textContent = announcement.description;
            announcementPhone.textContent = announcement.phone;

        } catch (error) {
            console.error("Error fetching announcement details:", error);
            alert("Failed to load announcement details.");
        }
    }

    // Load the announcement details
    fetchAnnouncementDetails(announcementId);

    // Back button functionality
    backBtn.addEventListener("click", () => {
        window.location.href = "homepage.html";
    });
});
