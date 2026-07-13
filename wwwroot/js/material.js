document.addEventListener("DOMContentLoaded", function () {
    const container = document.getElementById("caracteristicsContainer");
    const addButton = document.getElementById("btnAddCaracteristics");

    // Add Row Click Event
    addButton.addEventListener("click", function () {
        // Generate a unique id using uuid to prevent duplicates
        const id = crypto.randomUUID();
        const index = Math.max(...[...document.querySelectorAll('.caracteristics-row > .index')].map(x => parseInt(x.value))) + 1;

        // Create the row structure using template literal
        const rowHtml = `
                    <tr class="caracteristics-row">
                        <input class="index" type="hidden" name="Caracteristics.Index" value="${index}" />
                        <input type="hidden" name="Caracteristics[${index}].Id" value="${id}" />
                        <td>
                            <input name="Caracteristics[${index}].Name" class="form-control" />
                        </td>
                        <td>
                            <input name="Caracteristics[${index}].Value" class="form-control" />
                        </td>
                        <td>
                            <button type="button" class="btn btn-danger remove-row">Delete</button>
                        </td>
                    </tr>
                `;

        // Insert the new row into the table body
        container.insertAdjacentHTML('beforeend', rowHtml);
        toggleDeleteButtons();
    });

    // Delete Row Click Event (Event Delegation)
    container.addEventListener("click", function (e) {
        if (e.target && e.target.classList.contains("remove-row")) {
            const row = e.target.closest("tr");
            row.remove();
            toggleDeleteButtons();
        }
    });

    // Prevent deleting the last row to keep form data valid
    function toggleDeleteButtons() {
        const rows = container.querySelectorAll(".caracteristics-row");
        const deleteButtons = container.querySelectorAll(".remove-row");

        if (rows.length === 1) {
            deleteButtons[0].setAttribute("disabled", "true");
        } else {
            deleteButtons.forEach(btn => btn.removeAttribute("disabled"));
        }
    }

    toggleDeleteButtons();
});