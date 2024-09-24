document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("searchTitle");
    const tableBody = document.querySelector("#announcementsTable tbody");
    const addAnnouncementBtn = document.getElementById("addAnnouncementBtn");
    const modal = document.getElementById("announcementFormModal");
    const closeModalBtn = document.querySelector(".close");
    const announcementForm = document.getElementById("announcementForm");
    const formTitle = document.getElementById("formTitle");
    const announcementIdInput = document.getElementById("announcementId");
    let debounceTimeout;

    // Fetch and display announcements
    async function fetchAnnouncements(searchTitle = "") {
        try {
            let url = `/api/announcements?searchTitle=${encodeURIComponent(searchTitle)}`;
            let response = await fetch(url);

            if (!response.ok) {
                throw new Error("Failed to fetch announcements");
            }

            let announcements = await response.json();
            tableBody.innerHTML = "";

            if (announcements.length === 0) {
                tableBody.innerHTML = `<tr><td colspan="3">No announcements found</td></tr>`;
                return;
            }

            announcements.forEach(announcement => {
                let row = document.createElement("tr");
                row.innerHTML = `
                    <td><img src="${announcement.image}" class="announcement-img" alt="${announcement.title}"></td>
                    <td><a href="details.html?id=${announcement.id}">${announcement.title}</a></td>
                    <td>
                        <button class="editBtn" data-id="${announcement.id}">Edit</button>
                        <button class="deleteBtn" data-id="${announcement.id}">Delete</button>
                    </td>
                `;
                tableBody.appendChild(row);
            });

            // Attach events to edit and delete buttons
            document.querySelectorAll(".editBtn").forEach(button => {
                button.addEventListener("click", () => openEditForm(button.dataset.id));
            });
            document.querySelectorAll(".deleteBtn").forEach(button => {
                button.addEventListener("click", () => deleteAnnouncement(button.dataset.id));
            });
        } catch (error) {
            console.error("Error fetching announcements:", error);
        }
    }

    // Debounce function to limit API calls
    function debounce(func, delay) {
        return function () {
            const context = this;
            const args = arguments;
            clearTimeout(debounceTimeout);
            debounceTimeout = setTimeout(() => func.apply(context, args), delay);
        };
    }

    // Dynamic search handler
    searchInput.addEventListener("input", debounce(() => {
        const searchTitle = searchInput.value;
        fetchAnnouncements(searchTitle);  // Fetch announcements as the user types
    }, 300));  // Delay of 300ms

    // Add Announcement Modal
    addAnnouncementBtn.addEventListener("click", () => {
        formTitle.textContent = "Add Announcement";
        modal.style.display = "block";
        announcementForm.reset();
    });

    // Close Modal
    closeModalBtn.addEventListener("click", () => {
        modal.style.display = "none";
    });

    // Add or Update Announcement
    announcementForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        let formData = new FormData(announcementForm);
        let id = announcementIdInput.value;
        let method = id ? "PUT" : "POST";
        let url = id ? `/api/announcements/${id}` : "/api/announcements";

        try {
            let response = await fetch(url, {
                method: method,
                body: formData
            });

            if (!response.ok) {
                throw new Error("Failed to save announcement");
            }

            modal.style.display = "none";
            fetchAnnouncements();  // Refresh announcements after adding/updating
        } catch (error) {
            console.error("Error saving announcement:", error);
        }
    });

    // Delete Announcement
    async function deleteAnnouncement(id) {
        try {
            let response = await fetch(`/api/announcements/${id}`, { method: "DELETE" });
            if (!response.ok) {
                throw new Error("Failed to delete announcement");
            }

            fetchAnnouncements();  // Refresh announcements after deleting
        } catch (error) {
            console.error("Error deleting announcement:", error);
        }
    }

    // Open Edit Form
    async function openEditForm(id) {
        try {
            let response = await fetch(`/api/announcements/${id}`);
            if (!response.ok) {
                throw new Error("Failed to fetch announcement details");
            }

            let announcement = await response.json();
            formTitle.textContent = "Edit Announcement";
            announcementIdInput.value = announcement.id;
            document.getElementById("title").value = announcement.title;
            document.getElementById("description").value = announcement.description;
            document.getElementById("phone").value = announcement.phone;

            modal.style.display = "block";
        } catch (error) {
            console.error("Error fetching announcement for edit:", error);
        }
    }

    // Initial fetch to display all announcements
    fetchAnnouncements();
});
