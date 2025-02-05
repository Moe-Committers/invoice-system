namespace invoice_system.Utils.Exceptions;

public class InvalidCurrentPassword : Exception {
    public InvalidCurrentPassword(string message) : base(message){
        
    }
}