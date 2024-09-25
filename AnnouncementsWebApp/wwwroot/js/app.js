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

    // ცხრილში მონაცემების გამოტანა
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
                    <td><a class = "title-link" href="details.html?id=${announcement.id}">${announcement.title}</a></td>
                    <td class="actions-column">
                         <button class="editBtn" data-id="${announcement.id}">Edit</button>
                        <button class="deleteBtn" data-id="${announcement.id}">Delete</button>
                    </td>
                `;
                tableBody.appendChild(row);
            });

            // ვამატებთ რედაქტირების და წაშლის ღილაკებს
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

    // API-ს გამოძახების კონტროლი
    function debounce(func, delay) {
        return function () {
            const context = this;
            const args = arguments;
            // შესაბამისი დროის გასვლის შემდეგ ახალი API ქოლი იგზავნება და წინა ტაიმერის თავიდანა ათვლა ხდება.
            clearTimeout(debounceTimeout);
            debounceTimeout = setTimeout(() => func.apply(context, args), delay);
        };
    }

    // დინამიური ძებნის ფუნქცია
    searchInput.addEventListener("input", debounce(() => {
        const searchTitle = searchInput.value;
        fetchAnnouncements(searchTitle);
    }, 300));

    // განცხადების დამატების ღილაკზე დაჭერისას გამოდის ბლოკი მონაცემების შესატანად
    addAnnouncementBtn.addEventListener("click", () => {
        formTitle.textContent = "Add Announcement";
        modal.style.display = "block";
        announcementForm.reset();
    });

    // ვხურავთ ბლოკს მონაცემების შენახვის შემდეგ.
    closeModalBtn.addEventListener("click", () => {
        modal.style.display = "none";
    });

    // ფორმის შენახვის შემდეგ ღილაკის მიერ გამოძახებული ფუქნცია რომელიც მიღებული მონაცემების მიხედვით განასხვავებს http request-ს.
    // ვიყენებთ try-catch ბლოკს შეცდომის დასაფიქსირებლად.
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
            fetchAnnouncements();  // თავიდან ვიძახებთ რათა ცვლილება თუ დამატება აისახოს.
        } catch (error) {
            console.error("Error saving announcement:", error);
        }
    });

    // წაშლა განცხადების უნიკალური იდენტიფიკატორით, ვიყენებთ try-catch ბლოკს შეცდომის დასაფიქსირებლად.
    async function deleteAnnouncement(id) {
        try {
            let response = await fetch(`/api/announcements/${id}`, { method: "DELETE" });
            if (!response.ok) {
                throw new Error("Failed to delete announcement");
            }

            fetchAnnouncements();  // // თავიდან ვიძახებთ რათა ცვლილება აისახოს
        } catch (error) {
            console.error("Error deleting announcement:", error);
        }
    }

    // ვხსნით რედაქტირების ფორმას და ვიღებთ ინფორმაციას
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

            const fileInput = document.getElementById("image");
            fileInput.value = "";

            modal.style.display = "block";
        } catch (error) {
            console.error("Error fetching announcement for edit:", error);
        }
    }

    // ვიძახებთ თავდაპირველად ჩამონათვალს განცხადებების
    fetchAnnouncements();
});
