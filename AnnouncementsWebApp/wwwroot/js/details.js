document.addEventListener("DOMContentLoaded", async () => {
    const announcementImage = document.getElementById("announcementImage");
    const announcementTitle = document.getElementById("announcementTitle");
    const announcementDescription = document.getElementById("announcementDescription");
    const announcementPhone = document.getElementById("announcementPhone");
    const backBtn = document.getElementById("backBtn");

    // აიდის მნიშვნელობას ვიღებთ query string- იდან და ვინახავთთ ცვლადში.
    const urlParams = new URLSearchParams(window.location.search);
    const announcementId = urlParams.get("id");

    if (!announcementId) {
        alert("Announcement ID is missing");
        return;
    }

    // დეტალებს ვიღებთ შესაბამისი კონტროლერის მეთოდის გამოძახებით, ვიყენებთ try-catch ბლოკს შეცდომების დასაფიქსირებლად.
    async function fetchAnnouncementDetails(id) {
        try {
            const response = await fetch(`/api/announcements/${id}`);
            if (!response.ok) {
                throw new Error("Failed to fetch announcement details");
            }

            const announcement = await response.json();
            // ინფორმაციის მიღების შემდეგ ვინახავთ მონაცემებს
            announcementImage.src = announcement.image;
            announcementTitle.textContent = announcement.title;
            announcementDescription.textContent = announcement.description;
            announcementPhone.textContent = announcement.phone;

        } catch (error) {
            console.error("Error fetching announcement details:", error);
            alert("Failed to load announcement details.");
        }
    }

    // დეტალების გამოტანა
    fetchAnnouncementDetails(announcementId);

    // მთავარ გვერდზე დაბრუნების ღილაკი
    backBtn.addEventListener("click", () => {
        window.location.href = "index.html";
    });
});
