$(function () {

    //Filter foodtems
    $('#filterIcon').on("click",function () {
        $('.filterPanel').slideToggle();
    });

    // Enter key triggers search form
    $('#searchIcon').on("click",function () {
            $('.searchForm')[0].submit();
    });


   
 //mouse zoom to Food container
$('.Food-items').on('mouseenter touchstart', function () {
        $(this).addClass('zoom');
}).on('mouseleave touchend', function () {
        $(this).removeClass('zoom');
});

   
});


document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("feedbackForm");
    const successDiv = document.getElementById("feedbackSuccess");
    const feedbackList = document.getElementById("feedbackList");

    // Function to load feedback list via AJAX
    function loadFeedbackList() {
        fetch("/Home/FeedbackListPartial")
            .then(res => res.text())
            .then(html => {
                feedbackList.innerHTML = html;
            })
            .catch(err => {
                console.error("Failed to load feedback list:", err);
            });
    }

    // Load feedback list on page load
    loadFeedbackList();

    if (!form) return;

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const data = {
            name: form["Form.Name"].value.trim(),
            email: form["Form.Email"].value.trim(),
            rating: parseInt(form["Form.Rating"].value),
            comments: form["Form.Comments"].value.trim()
        };

        fetch("/Home/SubmitFeedbackAjax", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        })
            .then(response => {
                if (!response.ok) return response.json().then(err => Promise.reject(err));
                return response.json();
            })
            .then(result => {
                successDiv.classList.remove("d-none");
                successDiv.textContent = result.message;
                setTimeout(() => {
                    successDiv.classList.add("d-none");
                    successDiv.textContent = ""; // optional: clear the message text
                }, 5000);

                form.reset();

                // Reload feedback list
                loadFeedbackList();
            })
            .catch(error => {
                console.error("Feedback submission error:", error);
                alert("Something went wrong. Please try again.");
            });
    });
});
