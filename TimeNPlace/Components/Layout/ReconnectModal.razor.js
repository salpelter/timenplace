const reconnectModal = document.getElementById("components-reconnect-modal");
reconnectModal.addEventListener("components-reconnect-state-changed", handleReconnectStateChanged);

const retryButton = document.getElementById("components-reconnect-button");
retryButton.addEventListener("click", retry);

const resumeButton = document.getElementById("components-resume-button");
resumeButton.addEventListener("click", resume);

function handleReconnectStateChanged(event) {
    const state = event.detail.state;

    if (state === "show") {
        setState("Reconnecting", "Rejoining the server...", false, false);
        reconnectModal.showModal();
    } else if (state === "hide") {
        reconnectModal.close();
    } else if (state === "failed") {
        setState("Connection lost", "We could not reconnect automatically.", true, false);
        document.addEventListener("visibilitychange", retryWhenDocumentBecomesVisible);
    } else if (state === "rejected") {
        setState("Session expired", "Reloading the page...", false, false);
        location.reload();
    } else if (state === "paused") {
        setState("Session paused", "The server paused this session.", false, true);
    } else if (state === "resuming") {
        setState("Resuming", "Trying to resume your session...", false, false);
    } else if (state === "resume-failed") {
        setState("Resume failed", "Please try again or reload the page.", false, true);
    }
}

function setState(title, message, showRetry, showResume) {
    document.getElementById("components-reconnect-title").textContent = title;
    document.getElementById("components-reconnect-message").textContent = message;
    retryButton.hidden = !showRetry;
    resumeButton.hidden = !showResume;
}

async function retry() {
    document.removeEventListener("visibilitychange", retryWhenDocumentBecomesVisible);

    try {
        const successful = await Blazor.reconnect();
        if (!successful) {
            const resumeSuccessful = await Blazor.resumeCircuit();
            if (!resumeSuccessful) {
                location.reload();
            } else {
                reconnectModal.close();
            }
        }
    } catch {
        document.addEventListener("visibilitychange", retryWhenDocumentBecomesVisible);
    }
}

async function resume() {
    try {
        const successful = await Blazor.resumeCircuit();
        if (!successful) {
            location.reload();
        }
    } catch {
        setState("Resume failed", "Please try again or reload the page.", false, true);
    }
}

async function retryWhenDocumentBecomesVisible() {
    if (document.visibilityState === "visible") {
        await retry();
    }
}
