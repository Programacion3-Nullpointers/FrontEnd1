document.addEventListener('DOMContentLoaded', function () {
    var registroExitosoModal = document.getElementById('registroExitosoModal');

    if (registroExitosoModal) {
        // **CAMBIO AQUÍ:** Usa addEventListener directamente, sin jQuery
        registroExitosoModal.addEventListener('hidden.bs.modal', function () {
            // Cuando el modal se cierra, redirige a la página principal.
            window.location.href = '/Principal/Principal.aspx';
        });
    }
});