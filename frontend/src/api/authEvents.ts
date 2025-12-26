type UnauthorizedHandler = () => void;

class AuthEvents {
  private unauthorizedHandlers = new Set<UnauthorizedHandler>();

  onUnauthorized(handler: UnauthorizedHandler): () => void {
    this.unauthorizedHandlers.add(handler);

    // return unsubscribe function
    return () => {
      this.unauthorizedHandlers.delete(handler);
    };
  }

  emitUnauthorized(): void {
    for (const handler of this.unauthorizedHandlers) {
      handler();
    }
  }
}

export const authEvents = new AuthEvents();
