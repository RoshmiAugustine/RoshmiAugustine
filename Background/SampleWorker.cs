using System;

class PCISQueueDoesNotExistException : Exception
{
    public PCISQueueDoesNotExistException(string message) : base(message)
    {
    }
}

class PCISNoItemsInQueue : Exception
{
    public PCISNoItemsInQueue(string message) : base(message)
    {
    }
}