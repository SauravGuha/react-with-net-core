import { Box, Typography, Card, CardContent, TextField, Avatar } from "@mui/material";
import { Link, useParams } from "react-router-dom";
import { startSignalrActivityConnection } from '../../../SignalrClient/Client';
import { useEffect, useRef, useState, type FormEvent } from "react";
import type { HubConnection } from "@microsoft/signalr";
import type { ActivityResponse, ChatCommentSchema } from "../../../types";
import { eventDateString } from "../../../lib/common";

export default function ActivityDetailsChat() {

    const { id } = useParams();
    const [comments, setComments] = useState<ChatCommentSchema[]>([]);
    const connectionHub = useRef<HubConnection | undefined>(undefined);
    const textInput = useRef<HTMLInputElement | null>(null);

    useEffect(() => {
        let signalRConnection: HubConnection;

        // start connection inside an async IIFE
        (async () => {
            signalRConnection = await startSignalrActivityConnection(id!);
            signalRConnection.on("AllActivityComments", (response: ActivityResponse) => {
                const activityComments = response.value as ChatCommentSchema[];
                setComments(activityComments);
            });
            signalRConnection.on("CommentAdded", (response: ActivityResponse) => {
                const activityComments = response.value as ChatCommentSchema[];
                setComments(prev => {
                    const map = new Map<string, ChatCommentSchema>();
                    [...activityComments, ...prev].forEach(c => map.set(c.id, c));
                    return Array.from(map.values());
                });
                if (textInput.current && textInput.current instanceof HTMLTextAreaElement)
                    textInput.current.value = '';
            });
            await signalRConnection.start();
            connectionHub.current = signalRConnection;
            console.log("SignalR Connected");
        })();

        return () => {
            if (signalRConnection) {
                signalRConnection.stop();
                console.log("SignalR Disconnected");
            }
        };
    }, [id]);

    async function createCommand(value: FormEvent<HTMLFormElement>) {
        value.preventDefault();
        const formData = new FormData(value.currentTarget);
        const body = formData.get("comment");
        if (connectionHub.current) {
            const connectionObject = connectionHub.current;
            await connectionObject.send('CreateComment', {
                activityId: id,
                body
            });
        }
    }

    return (
        <>
            <Box
                sx={{
                    textAlign: 'center',
                    bgcolor: 'primary.main',
                    color: 'white',
                    padding: 2
                }}
            >
                <Typography variant="h6">Chat about this event</Typography>
            </Box>
            <Card>
                <CardContent>
                    <div>
                        <form onSubmit={createCommand}>
                            <TextField
                                inputRef={textInput}
                                id='comment'
                                name="comment"
                                variant="outlined"
                                fullWidth
                                multiline
                                rows={2}
                                placeholder="Enter your comment (Enter to submit, SHIFT + Enter for new line)"
                                onKeyDown={(e) => {
                                    if (e.key === "Enter" && !e.shiftKey) {
                                        e.preventDefault();
                                        if (e.currentTarget.parentElement instanceof HTMLFormElement) {
                                            e.currentTarget.parentElement?.requestSubmit();
                                        }
                                    }
                                }}
                            />
                        </form>
                    </div>

                    <Box>
                        {comments.map(item =>
                            <Box sx={{ display: 'flex', my: 2 }} key={item.id}>
                                <Avatar src={item.imageUrl ? item.imageUrl : '/images/user.png'}
                                    alt={item.imageUrl ? item.imageUrl : 'user image'} sx={{ mr: 2 }} />
                                <Box display='flex' flexDirection='column'>
                                    <Box display='flex' alignItems='center' gap={3}>
                                        <Typography component={Link} to={`/profiles/${item.userId}`}
                                            variant="subtitle1" sx={{ fontWeight: 'bold', textDecoration: 'none' }}>
                                            {item.displayName}
                                        </Typography>
                                        <Typography variant="body2" color="textSecondary">
                                            {eventDateString(item.createdAt)}
                                        </Typography>
                                    </Box>

                                    <Typography sx={{ whiteSpace: 'pre-wrap' }}>{item.body}</Typography>
                                </Box>
                            </Box>)}
                    </Box>
                </CardContent>
            </Card >
        </>
    )
}